using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using CashMachineWeb.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;

namespace CashMachineWeb.Controllers
{
    public class CreditCardAccountController : Controller
    {
        private readonly UserManager<CreditCardAccount> accountManager;

        public CreditCardAccountController()
        {
            accountManager = new UserManager<CreditCardAccount>(new UserStore<CreditCardAccount>(new CashMachineDbContext()));
        }
        //
        // GET: /CreditCardAccount/InputCardNumber
        public ActionResult InputCardNumber()
        {
            return View();
        }

        //
        // POST: /CreditCardAccount/InputCardNumber
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> InputCardNumber(CreditCardModel model)
        {
            if (ModelState.IsValid)
            {
                var account = await accountManager.FindByNameAsync(model.ActualNumber);
                if (account != null)
                {
                    TempData["CreditCardNumber"] = model;
                    return RedirectToAction("InputPinNumber");
                }
                else
                {
                    ModelState.AddModelError("", "Card number is not found");
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        public ActionResult InputPinNumber()
        {
            var model = TempData["CreditCardNumber"];
	        if (model == null)
	        {
				return RedirectToAction("InputCardNumber");
	        }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> InputPinNumber(CreditCardModel model)
        {
            if (ModelState.IsValid)
            {
                var account = await accountManager.FindAsync(model.ActualNumber, model.Pin);
                if (account != null)
                {
                    var identity = await accountManager.CreateIdentityAsync(account, DefaultAuthenticationTypes.ApplicationCookie);
                    HttpContext.GetOwinContext().Authentication.SignIn(new AuthenticationProperties(), identity);
                    return RedirectToAction("Index", "Operations");
                }
                else
                {
                    ModelState.AddModelError("", "Invalid pin");
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

		[Authorize]
	    public ActionResult SignOut()
	    {
		    HttpContext.GetOwinContext().Authentication.SignOut();
			return RedirectToAction("InputCardNumber");
	    }
    }
}