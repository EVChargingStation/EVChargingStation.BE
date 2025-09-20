using EVChargingStation.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace EVChargingStation.Domain.Entities
{
    public class Payment : BaseEntity
    {
        [Required]
        [Range(0, 1000000)]
        public decimal Amount { get; set; }
        
        public PaymentStatus Status { get; set; } = PaymentStatus.Pending;
        
        public string? Note { get; set; }
        
        public int? InvoiceId { get; set; }
        
        public int? SessionId { get; set; }
        
        public int UserId { get; set; }

        // Navigation properties
        public User User { get; set; } = null!;
        public Invoice? Invoice { get; set; }
        public Session? Session { get; set; }
    }
}
