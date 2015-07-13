using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using CashMachineWeb.Models;

namespace CashMachineWeb.Controllers
{
    public class CreditCardAccountController : Controller
    {
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
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }
    }
}