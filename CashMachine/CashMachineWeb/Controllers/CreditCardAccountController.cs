﻿using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using CashMachineWeb.Database;
using CashMachineWeb.Domain;
using CashMachineWeb.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;

namespace CashMachineWeb.Controllers
{
    public class CreditCardAccountController : Controller
    {
        private readonly UserManager<CreditCardAccount> accountManager;
	    private readonly IAccountSecurityManager accountSecurity;

        public CreditCardAccountController()
        {
			// in a real production system we would be asking for abstract dependencies in a constructor
			// while resolving them via DI/IoC Container of choice or manually via IControllerFactory / DefaultControllerFactory implementation / override
			// instead of manually new-uping them in a default parameter-less constructor 
			// to keep our controller decoupled from implementations and easily testable

			// also we would like to keep our 'business logic' (i.e. number of attempts allowed, when to block card, etc) away from Controller 
			// to keep things separated, easy to maintain, test and evolve independently

			// right now it is too heavy on the conditional logic (if operators), though all the navigation and talking to a database stuff could be also tested
			// if provided mock-able abstractions instead of concrete dependencies on the Identity library
			
            accountManager = new UserManager<CreditCardAccount>(new UserStore<CreditCardAccount>(new CashMachineDbContext()));
			accountSecurity = new AccountSecurityManager();
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
		public async Task<ActionResult> InputCardNumber(CreditCardNumberViewModel model)
        {
            if (ModelState.IsValid)
            {
                var account = await accountManager.FindByNameAsync(model.ActualNumber);
                if (account != null)
                {
	                if (account.IsBlocked)
	                {
		                return RedirectToError("Account it blocked for the card you've entered!");
	                }
                    
					// all is fine, processing to the PIN page
					TempData["CreditCardNumber"] = model;
                    return RedirectToAction("InputPinNumber");
                }

	            return RedirectToError("Card number is not found!");
            }

            // If we got this far, validation failed, redisplay form
            return View(model);
        }

        public ActionResult InputPinNumber()
        {
			var cardNumberModel = TempData["CreditCardNumber"] as CreditCardNumberViewModel;
	        if (cardNumberModel == null)
	        {
				return RedirectToAction("InputCardNumber");
	        }

	        var model = new CreditCardViewModel
	        {
		        ActualNumber = cardNumberModel.ActualNumber,
		        DisplayNumber = cardNumberModel.DisplayNumber
	        };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
		public async Task<ActionResult> InputPinNumber(CreditCardViewModel model)
        {
            if (ModelState.IsValid)
            {
                var account = await accountManager.FindAsync(model.ActualNumber, model.Pin);
                if (account != null)
                {
					// success!
					// reset failed attempts
					accountSecurity.ProcessCorrectPinInput(account);
					await accountManager.UpdateAsync(account); // for simplicity's-sake we won't check for success of the operation here, just hoping for the best :)

					// sign-in account
                    var identity = await accountManager.CreateIdentityAsync(account, DefaultAuthenticationTypes.ApplicationCookie);
                    HttpContext.GetOwinContext().Authentication.SignIn(new AuthenticationProperties(), identity);
                    return RedirectToAction("Index", "Operations");
                }
	            
				// have to process incorrect PIN input
	            account = await accountManager.FindByNameAsync(model.ActualNumber);
	            if (account != null)
	            {
					accountSecurity.ProcessIncorrectPinInput(account);
					await accountManager.UpdateAsync(account); // for simplicity's-sake we won't check for success of the operation here, just hoping for the best :)

		            if (account.IsBlocked)
		            {
			            return RedirectToError(
				            string.Format("Incorrect PIN input attempts exceeded. Account for the card {0} has been blocked",
					            account.UserName));
		            }
	            }
				ModelState.AddModelError("", "Invalid pin");
            }

            // If we got this far, validation failed, redisplay form
            return View(model);
        }

		[Authorize]
		[HttpPost]
		[ValidateAntiForgeryToken]
	    public ActionResult SignOut()
	    {
		    HttpContext.GetOwinContext().Authentication.SignOut();
			return RedirectToAction("InputCardNumber");
	    }

		private RedirectToRouteResult RedirectToError(string errorMessage)
	    {
		    TempData["ErrorMessage"] = errorMessage;
		    return RedirectToAction("Index", "Error");
	    }
    }
}