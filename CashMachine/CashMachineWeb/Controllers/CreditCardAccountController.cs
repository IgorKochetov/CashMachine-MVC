using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CashMachineWeb.Controllers
{
    public class CreditCardAccountController : Controller
    {
        //
        // GET: /CreditCardAccount/
        public ActionResult InputCardNumber()
        {
            return View();
        }
	}
}