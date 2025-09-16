namespace EVChargingStation.Domain.Entities
{
    public class Payment : BaseEntity
    {
        public Guid UserId { get; set; }
        public Guid SessionId { get; set; }
        public Guid? InvoiceId { get; set; }
        public decimal Amount { get; set; }
        public string Status { get; set; }
        public string Note { get; set; }

        // Navigation property
        public User User { get; set; }
        public Invoice Invoice { get; set; }
        public Session Session { get; set; }
    }
}
