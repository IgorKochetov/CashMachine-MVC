﻿using System.Web.Mvc;

namespace CashMachineWeb.Controllers
{
	[Authorize]
	public class OperationsController : Controller
	{
		public ActionResult Index()
		{
			var name = User.Identity.Name; // debug
			return View();
		}
	}
}