using System.Diagnostics;
using CashMachineWeb.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace CashMachineWeb.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<CashMachineWeb.Models.CashMachineDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "CashMachineWeb.Models.CashMachineDbContext";
        }

        protected override void Seed(CashMachineWeb.Models.CashMachineDbContext context)
        {
			// uncomment if we need to debug Seed method while running Update from PackageManager Console
			//if (System.Diagnostics.Debugger.IsAttached == false)
			//	System.Diagnostics.Debugger.Launch();

	        var accountManager = new UserManager<CreditCardAccount>(new UserStore<CreditCardAccount>(context))
	        {
		        PasswordValidator = new MinimumLengthValidator(4)
	        };

			// we may want to protect ourselves from repeated attempts to insert already existed record
			// on every new run of a seed method by querying for the 'Name' aka CardNumber first,
			// but since UserManager will handle it for us and just return unsuccessful result on create
			// we might just skip it and save us some coding

			// also we don't really need a result here, again, using for debug only to see results

	        var identityResult = accountManager.Create(new CreditCardAccount {UserName = "1111111111111111"}, "1111");
			identityResult = accountManager.Create(new CreditCardAccount { UserName = "2222222222222222" }, "2222");
			identityResult = accountManager.Create(new CreditCardAccount { UserName = "3333333333333333" }, "3333");
			identityResult = accountManager.Create(new CreditCardAccount { UserName = "4444444444444444" }, "4444");

	        
        }
    }
}
