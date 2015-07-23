using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CashMachineWeb.Models
{
	public class WithdrawModel
	{
		[Required]
		public decimal WithdrawAmount { get; set; }
	}
}