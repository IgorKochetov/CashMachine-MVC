using System.Collections.Generic;
using CashMachineWeb.Domain;
using Microsoft.AspNet.Identity.EntityFramework;

namespace CashMachineWeb.Models
{
	public class CreditCardAccount : IdentityUser
	{
		public bool IsBlocked { get; set; }
		public byte IncorrectPinInputCounter { get; set; }

		public decimal Balance { get; set; }

		public virtual IList<OperationLog> Operations { get; set; }
	}
}