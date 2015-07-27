using System.ComponentModel.DataAnnotations;

namespace CashMachineWeb.Models
{
	public class WithdrawViewModel
	{
		[Required]
		public decimal WithdrawAmount { get; set; }
	}
}