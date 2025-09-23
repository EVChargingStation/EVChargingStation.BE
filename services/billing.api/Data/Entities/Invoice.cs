using System.ComponentModel.DataAnnotations;
using billing.api.Data.Enums;
using Shared.Kernel.Entities;

namespace billing.api.Data.Entities
{
    public class Invoice : BaseEntity
    {
        [Required]
        public Guid UserId { get; set; }
        
        public Guid? SessionId { get; set; }
        
        [Required]
        public DateTime PeriodStart { get; set; }
        
        [Required]
        public DateTime PeriodEnd { get; set; }
        
        public InvoiceStatus Status { get; set; } = InvoiceStatus.Draft;
        
        [Range(0, 1000000)]
        public decimal SubtotalAmount { get; set; } = 0;
        
        [Range(0, 1000000)]
        public decimal TaxAmount { get; set; } = 0;
        
        [Range(0, 1000000)]
        public decimal TotalAmount { get; set; } = 0;
        
        [Range(0, 1000000)]
        public decimal AmountPaid { get; set; } = 0;
        
        public DateTime? DueDate { get; set; }
        
        public DateTime? IssuedAt { get; set; }

        // Navigation properties - Cross-service references removed
        // User, Sessions are referenced by ID only
        // public User User { get; set; } = null!;
        // public ICollection<Session> Sessions { get; set; } = new List<Session>();
        
        // Navigation within same bounded context
        public ICollection<Payment> Payments { get; set; } = new List<Payment>();
    }
}