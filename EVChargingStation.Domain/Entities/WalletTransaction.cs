using EVChargingStation.Domain.Enums;

namespace EVChargingStation.Domain.Entities
{
    public class WalletTransaction : BaseEntity
    {
        public Guid WalletId { get; set; }
        public long Amount { get; set; }
        public WalletType Type { get; set; }

        // Optional references to other entities (nếu cần)
        public string Reference { get; set; }   
        public Guid? PaymentId { get; set; }
        public Guid? SessionId { get; set; }
        public Guid? InvoiceId { get; set; }

        // Navigation property
        public Wallet Wallet { get; set; }
        public Payment Payment { get; set; }
        public Session Session { get; set; }
        public Invoice Invoice { get; set; }
    }
}
