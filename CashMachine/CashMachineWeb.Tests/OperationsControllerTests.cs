using System;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using CashMachineWeb.Controllers;
using CashMachineWeb.Domain;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;

namespace CashMachineWeb.Tests
{
	[TestFixture]
	class OperationsControllerTests
	{
		// example of how we can test Controller for its Actions
		// also, we are doing 
		// in order to test Balance() or Withdraw() to really play with our mock, we should also abstract away dependency on User.Identity
		// which comes from httpContext from ControllerContext or substitute it also - doable but verbose for this sample

		private const string AccountNumber = "1111111111111111";


		// the simplest one here (OperationReport), having no dependencies other than TempData
		[Test]
		public void OperationReport_ShouldReturnTempDataOperationResultAsModel()
		{
			// arrange
			// we should not have default constructor anyway, and we cannot use it since we really want to isolate our tests from infrastructure (database, in this case)
			// using mocking library to provide test doubles
			IAccountOperationsService accountOperationsService = Substitute.For<IAccountOperationsService>();
			var controller = new OperationsController(accountOperationsService); 
			var operationResult = new OperationResult
			{
				AmountWithdrawed = 100,
				BalanceLeftAmount = 200,
				CardNumber = AccountNumber,
				Timestamp = DateTime.MinValue,
			};
			controller.TempData["OperationResult"] = operationResult;
			
			// act
			var actionResult = controller.OperationReport();

			// assert
			((ViewResultBase)(actionResult)).Model.ShouldBeEquivalentTo(operationResult);
		}
		
		[Test]
		public void Balance_ShouldAskAccountOperationServiceForBalance_UsingProperAccountNumber()
		{
			// arrange
			var accountOperationsService = Substitute.For<IAccountOperationsService>();
			var controller = new OperationsController(accountOperationsService);
			MockUserIdentityForController(controller);

			// act
			controller.Balance();

			// assert
			accountOperationsService.Received().GetBalanceForAccount(AccountNumber);
		}

		[Test]
		public void Balance_ShouldReturnProperBalanceStampAsModel_FromAccountOperationService()
		{
			// arrange
			var accountOperationsService = Substitute.For<IAccountOperationsService>();
			AccountBalanceStamp balanceStamp = new AccountBalanceStamp
			{
				CardNumber = AccountNumber,
				MoneyAmount = 300,
				Timestamp = DateTime.MaxValue,
			};
			accountOperationsService.GetBalanceForAccount(Arg.Any<string>()).Returns(balanceStamp);

			var controller = new OperationsController(accountOperationsService);
			MockUserIdentityForController(controller);

			// act
			var actionResult = controller.Balance();

			// assert
			accountOperationsService.Received().GetBalanceForAccount(AccountNumber);
			((ViewResultBase)(actionResult)).Model.ShouldBeEquivalentTo(balanceStamp);
		}

		// in order to test Balance() or Withdraw() to really play with our mock, we should also abstract away dependency on User.Identity
		private void MockUserIdentityForController(Controller controller)
		{
			var context = Substitute.For<HttpContextBase>();
			var user = Substitute.For<IPrincipal>();
			context.User.Returns(user);
			var identity = Substitute.For<IIdentity>();
			user.Identity.Returns(identity);
			identity.Name.Returns(AccountNumber);

			controller.ControllerContext = new ControllerContext { HttpContext = context };
		}
	}
}
