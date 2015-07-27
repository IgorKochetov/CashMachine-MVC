using System.Data.Entity.Migrations;
using CashMachineWeb.Database;
using CashMachineWeb.Domain;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace CashMachineWeb.Migrations
{
	internal sealed class Configuration : DbMigrationsConfiguration<CashMachineDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(CashMachineDbContext context)
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

			var identityResult = accountManager.Create(new CreditCardAccount { UserName = "1111111111111111", Balance = 1000, IsBlocked = false, IncorrectPinInputCounter = 0}, "1111");
			identityResult = accountManager.Create(new CreditCardAccount { UserName = "2222222222222222", Balance = 2000, IsBlocked = false, IncorrectPinInputCounter = 0 }, "2222");
			identityResult = accountManager.Create(new CreditCardAccount { UserName = "3333333333333333", Balance = 3000, IsBlocked = false, IncorrectPinInputCounter = 0 }, "3333");
			identityResult = accountManager.Create(new CreditCardAccount { UserName = "4444444444444444", Balance = 4000, IsBlocked = false, IncorrectPinInputCounter = 0 }, "4444");
        }
    }
}
