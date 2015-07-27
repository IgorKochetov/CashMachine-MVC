using System.Collections.Generic;
using Microsoft.AspNet.Identity.EntityFramework;

namespace CashMachineWeb.Domain
{
	public class CreditCardAccount : IdentityUser
	{
		public bool IsBlocked { get; set; }
		public byte IncorrectPinInputCounter { get; set; }

		public decimal Balance { get; set; }

		// this guy sits here specifically for EF relations to build a proper FK constraint; no need for that navigation property otherwise (at least not right now)
		// also making it virtual to prevent eager loading (making it as lazy as possible)
		public virtual IList<OperationLog> Operations { get; set; }
	}
}