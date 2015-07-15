using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using CashMachineWeb.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

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
        [AllowAnonymous]
        public ActionResult InputCardNumber()
        {
            return View();
        }

        //
        // POST: /CreditCardAccount/InputCardNumber
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> InputCardNumber(CreditCardNumber model)
        {
            if (ModelState.IsValid)
            {
	            var account = await accountManager.FindByNameAsync(model.ActualNumber);
	            if (account != null)
	            {
		            RedirectToAction("InputPinNumber", new {cardNumber = model.ActualNumber});
	            }
	            else
	            {
		            ModelState.AddModelError("", "Card number is not found");
	            }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }
		
	    public ActionResult InputPinNumber(string cardNumber)
	    {
		    return View();
	    }
    }
}