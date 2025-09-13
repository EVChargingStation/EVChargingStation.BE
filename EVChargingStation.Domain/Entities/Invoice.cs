namespace EVChargingStation.Domain.Entities
{
    public class Invoice : BaseEntity
    {
        public Guid UserId { get; set; }
        public DateTime PeriodStart { get; set; }
        public DateTime PeriodEnd { get; set; }
        public string Status { get; set; }
        public decimal SubtotalAmount { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal AmountPaid { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime IssuedAt { get; set; }

        // Navigation property
        public User User { get; set; }
        public ICollection<Payment> Payments { get; set; }
    }

}
