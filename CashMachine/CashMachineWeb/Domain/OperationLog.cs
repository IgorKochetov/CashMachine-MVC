using System;
using System.ComponentModel.DataAnnotations;

namespace CashMachineWeb.Domain
{
    public class OperationLog
    {
        public long Id { get; set; }
        [Required]
		public string CreditCardAccountId { get; set; }
        [Required]
        public DateTime Timestamp { get; set; }
        [Required]
        public OperationCode Code { get; set; }
        public string Payload { get; set; }
    }
}