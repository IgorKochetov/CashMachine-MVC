using System.Web.Mvc;

namespace CashMachineWeb.Controllers
{
	[Authorize]
	public class OperationsController : Controller
	{
		public OperationsController()
		{
			// in a real production system we would be asking for abstract dependencies in a constructor
			// while resolving them via DI/IoC Container of choice
			// instead of manually new-uping them in a default parameter-less constructor 
			// to keep our controller decoupled from implementations and easily testable
		}
		public ActionResult Index()
		{
			var name = User.Identity.Name; // debug
			return View();
		}

		public ActionResult Balance()
		{
			return View();
		}

		public ActionResult Withdraw()
		{
			return View();
		}
	}
}