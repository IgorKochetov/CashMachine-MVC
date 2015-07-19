using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CashMachineWeb.Models
{
    public class CreditCardModel
    {
		//[Required]
		//[Display(Name = "Card number")]
		//[StringLength(maximumLength:4, ErrorMessage = "Should contain 4 digits", MinimumLength = 4)]
		//public string PartOne { get; set; }

		//[Required]
		//[Display(Name = "Card number")]
		//[StringLength(maximumLength: 4, ErrorMessage = "Should contain 4 digits", MinimumLength = 4)]
		//public string PartTwo { get; set; }

		//[Required]
		//[Display(Name = "Card number")]
		//[StringLength(maximumLength: 4, ErrorMessage = "Should contain 4 digits", MinimumLength = 4)]
		//public string PartThree { get; set; }

		//[Required]
		//[Display(Name = "Card number")]
		//[StringLength(maximumLength: 4, ErrorMessage = "Should contain 4 digits", MinimumLength = 4)]
		//public string PartFour { get; set; }

		[Required]
		[Display(Name = "Card number")]
		[StringLength(maximumLength: 16, ErrorMessage = "Should contain 16 digits", MinimumLength = 16)]
		public string ActualNumber { get; set; }

		[Required]
		[Display(Name = "Card number")]
		[StringLength(maximumLength: 19, ErrorMessage = "Should contain 16 digits", MinimumLength = 19)]
		public string DisplayNumber { get; set; }

        [Display(Name = "Pin number")]
        [StringLength(maximumLength: 4, ErrorMessage = "Should contain 4 digits", MinimumLength = 4)]
        public string Pin { get; set; }
    }
}