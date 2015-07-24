using System;

namespace CashMachineWeb.Models
{
	public class OperationResult
	{
		public string CardNumber { get; set; }
		public DateTime Timestamp { get; set; }
		public decimal AmountWithdrawed { get; set; }
		public decimal BalanceLeftAmount { get; set; }
	}
}