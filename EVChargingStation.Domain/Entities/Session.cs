namespace EVChargingStation.Domain.Entities
{
    public class Session : BaseEntity
    {
        public Guid ReservationId { get; set; }
        public Guid ConnectorId { get; set; }
        public Guid UserId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Status { get; set; }
        public decimal? SocStart { get; set; }
        public decimal? SocEnd { get; set; }
        public decimal? EnergyKwh { get; set; }
        public decimal Cost { get; set; }
        public Guid? InvoiceId { get; set; }

        // Navigation property
        public Reservation Reservation { get; set; }
        public Connector Connector { get; set; }
        public User User { get; set; }
        public Invoice Invoice { get; set; }
        public ICollection<Payment> Payments { get; set; }
    }
}
