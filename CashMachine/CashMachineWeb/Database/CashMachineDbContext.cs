using System.Data.Entity;
using CashMachineWeb.Domain;
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

        public DbSet<OperationLog> OperationLogs { get; set; }

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			modelBuilder.Entity<CreditCardAccount>().ToTable("CreditCardAccount");
			modelBuilder.Entity<IdentityUser>().ToTable("CreditCardAccount");
			modelBuilder.Entity<IdentityUser>().Property(u=>u.UserName).HasColumnName("CardNumber");
			modelBuilder.Entity<IdentityUser>().Property(u => u.PasswordHash).HasColumnName("PinHash");
			modelBuilder.Entity<OperationLog>().ToTable("OperationLog");

			var ignoreTypes = new[]
			{
				typeof (IdentityUserLogin),
				typeof (IdentityUserRole),
				typeof(IdentityRole),
				typeof(IdentityUserClaim),
			};
			modelBuilder.Ignore(ignoreTypes);

		}
	}
}