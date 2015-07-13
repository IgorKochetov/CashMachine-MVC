using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CashMachineWeb.Models
{
    public class CreditCardNumber
    {
        [Required]
        [Display(Name = "Card number")]
        [StringLength(maximumLength:4, ErrorMessage = "Should contain 4 digits", MinimumLength = 4)]
        public string PartOne { get; set; }

        [Required]
        [Display(Name = "Card number")]
        [StringLength(maximumLength: 4, ErrorMessage = "Should contain 4 digits", MinimumLength = 4)]
        public string PartTwo { get; set; }

        [Required]
        [Display(Name = "Card number")]
        [StringLength(maximumLength: 4, ErrorMessage = "Should contain 4 digits", MinimumLength = 4)]
        public string PartThree { get; set; }

        [Required]
        [Display(Name = "Card number")]
        [StringLength(maximumLength: 4, ErrorMessage = "Should contain 4 digits", MinimumLength = 4)]
        public string PartFour { get; set; }

    }
}