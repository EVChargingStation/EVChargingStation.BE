namespace EVChargingStation.Domain.Entities
{
    public class Reservation : BaseEntity
    {
        public Guid UserId { get; set; }
        public Guid ConnectorId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public string Status { get; set; }

        // Navigation property
        public User User { get; set; }
        public Connector Connector { get; set; }
        public Session Session { get; set; }
    }
}
