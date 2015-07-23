using System.Web.Mvc;

namespace CashMachineWeb.Controllers
{
    public class ErrorController : Controller
    {
        //
        // GET: /Error/
        public ActionResult Index()
        {
	        var errorMessage = TempData["ErrorMessage"];
	        ViewBag.ErrorMessage = errorMessage;
	        return View();
        }
	}
}