using System.ComponentModel.DataAnnotations;
using billing.api.Data.Enums;

namespace billing.api.Data.Entities
{
    public class Payment : BaseEntity
    {
        [Required]
        [Range(0, 1000000)]
        public decimal Amount { get; set; }
        
        public PaymentStatus Status { get; set; } = PaymentStatus.Pending;
        
        public string? Note { get; set; }
        
        public Guid? InvoiceId { get; set; }
        
        public Guid? SessionId { get; set; }
        
        public Guid UserId { get; set; }

        // Navigation properties - Cross-service references removed
        // User, Session are referenced by ID only
        // public User User { get; set; } = null!;
        // public Session? Session { get; set; }
        
        // Navigation within same bounded context
        public Invoice? Invoice { get; set; }
    }
}