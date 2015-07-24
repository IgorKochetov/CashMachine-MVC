using System.ComponentModel.DataAnnotations;

namespace CashMachineWeb.Models
{
    public class CreditCardNumberViewModel
    {
		[Required]
		[Display(Name = "Card number")]
		[StringLength(maximumLength: 16, ErrorMessage = "Should contain 16 digits", MinimumLength = 16)]
		public string ActualNumber { get; set; }

		[Required]
		[Display(Name = "Card number")]
		[StringLength(maximumLength: 19, ErrorMessage = "Should contain 16 digits", MinimumLength = 19)]
		public string DisplayNumber { get; set; }
    }
}