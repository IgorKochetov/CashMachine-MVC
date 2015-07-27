using System;
using System.Web.Mvc;
using CashMachineWeb.Database;
using CashMachineWeb.Domain;
using CashMachineWeb.Models;

namespace CashMachineWeb.Controllers
{
	[Authorize]
	public class OperationsController : Controller
	{
		private readonly IAccountOperationsService operationsService;

		public OperationsController(IAccountOperationsService operationsService)
		{
			this.operationsService = operationsService;
		}

		// Bastard-Injection DI anti-pattern! Never do it in real systems, used here just for simplicity and testability
		// asume it is not there for design and usage purposes
		public OperationsController() : this(new AccountOperationsService(new CashMachineDbContext()))
		{
		}

		public ActionResult Index()
		{
			return View();
		}

		public ActionResult Balance()
		{
			var balanceModel = operationsService.GetBalanceForAccount(AccountNumber);
			return View(balanceModel);
		}

		public ActionResult Withdraw()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Withdraw(WithdrawViewModel viewModel)
		{
			if (ModelState.IsValid)
			{
				try
				{
					OperationResult operationResult = operationsService.WithdrawMoney(AccountNumber, viewModel.WithdrawAmount);
					TempData["OperationResult"] = operationResult;
					return RedirectToAction("OperationReport");
				}
				catch (AmountExceededException exception)
				{
					// ideally we do some logging than display message to a user
					TempData["ErrorMessage"] =
						"Your request could not be proceed. You've exceeded your account limit on that operation request";
					return RedirectToAction("Index", "Error");
				}

			}

			// If we got this far, validation failed, redisplay form
			return View(viewModel);
		}

		public ActionResult OperationReport()
		{
			var operationResult = TempData["OperationResult"] as OperationResult;
			return View(operationResult);
		}

		private string AccountNumber
		{
			get { return User.Identity.Name; }
		}
	}
}