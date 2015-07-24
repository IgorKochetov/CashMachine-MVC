using System.ComponentModel.DataAnnotations;

namespace CashMachineWeb.Models
{
	public class CreditCardViewModel
	{
		[Required]
		[Display(Name = "Card number")]
		[StringLength(maximumLength: 16, ErrorMessage = "Should contain 16 digits", MinimumLength = 16)]
		public string ActualNumber { get; set; }

		[Required]
		[Display(Name = "Card number")]
		[StringLength(maximumLength: 19, ErrorMessage = "Should contain 16 digits", MinimumLength = 19)]
		public string DisplayNumber { get; set; }

		[Required]
		[Display(Name = "Pin number")]
		[StringLength(maximumLength: 4, ErrorMessage = "Should contain 4 digits", MinimumLength = 4)]
		public string Pin { get; set; }
	}
}