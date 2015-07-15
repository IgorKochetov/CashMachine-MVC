﻿using Microsoft.AspNet.Identity.EntityFramework;

namespace CashMachineWeb.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection")
        {
        }
    }

	public class CreditCardAccount : IdentityUser
	{
	}

	public class CashMachineDbContext : IdentityDbContext<CreditCardAccount>
	{
		public CashMachineDbContext()
			: base("DefaultConnection")
		{
			
		}
	}
}