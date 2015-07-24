using CashMachineWeb.Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace CashMachineWeb.Database
{
	public class CashMachineDbContext : IdentityDbContext<CreditCardAccount>
	{
		public CashMachineDbContext()
			: base("DefaultConnection")
		{

		}
	}
}