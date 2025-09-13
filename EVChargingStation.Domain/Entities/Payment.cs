namespace EVChargingStation.Domain.Entities
{
    public class Payment : BaseEntity
    {
        public Guid UserId { get; set; }
        public decimal Amount { get; set; }
        public string Method { get; set; }
        public string Status { get; set; }
        public Guid? SessionId { get; set; }
        public Guid? InvoiceId { get; set; }
        public Guid? WalletTransactionId { get; set; }
        public string Note { get; set; }

        // Navigation property
        public User User { get; set; }
        public Session Session { get; set; }
        public Invoice Invoice { get; set; }
        public WalletTransaction WalletTransaction { get; set; }
    }
}
