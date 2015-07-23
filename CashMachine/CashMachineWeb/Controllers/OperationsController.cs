using System.Web.Mvc;
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
			// while resolving them via DI/IoC Container of choice
			// instead of manually new-uping them in a default parameter-less constructor 
			// to keep our controller decoupled from implementations and easily testable
			operationsService = new AccountOperationsServiceStub();
		}
		public ActionResult Index()
		{
			return View();
		}

		public ActionResult Balance()
		{
			var accountNumber = User.Identity.Name;
			var balanceModel = operationsService.GetBalanceForAccount(accountNumber);
			return View(balanceModel);
		}

		public ActionResult Withdraw()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Withdraw(WithdrawModel model)
		{
			if (ModelState.IsValid)
			{
				
				TempData["ErrorMessage"] =
					"Your request could not be proceed. You've exceeded your account limit on that operation request";
				return RedirectToAction("Index", "Error");
			}

			// If we got this far, something failed, redisplay form
			return View(model);
		}
	}
}