using System;

namespace CashMachineWeb.Domain
{
	public class AccountBalanceStamp
	{
		public string CardNumber { get; set; }
		public decimal MoneyAmount { get; set; }
		public DateTime Timestamp { get; set; }
	}
}