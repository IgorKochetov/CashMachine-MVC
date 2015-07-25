using System;
using System.ComponentModel.DataAnnotations;

namespace CashMachineWeb.Models
{
    public class OperationLog
    {
        public long Id { get; set; }
        [Required]
        public string AccountId { get; set; }
        [Required]
        public DateTime Timestamp { get; set; }
        [Required]
        public OperationCode Code { get; set; }
        public string Payload { get; set; }
    }
}