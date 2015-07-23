using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CashMachineWeb.Models
{
	public class BalanceModel
	{
		public string CardNumber { get; set; }
		public decimal MoneyAmount { get; set; }
		public DateTime Timestamp { get; set; }
	}
}