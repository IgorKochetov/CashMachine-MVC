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
		public OperationsController()
		{
			// in a real production system we would be asking for abstract dependencies in a constructor
			// while resolving them via DI/IoC Container of choice or manually via IControllerFactory / DefaultControllerFactory implementation / override
			// instead of manually new-uping them in a default parameter-less constructor 
			// to keep our controller decoupled from implementations and easily testable
			operationsService = new AccountOperationsService(new CashMachineDbContext());
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