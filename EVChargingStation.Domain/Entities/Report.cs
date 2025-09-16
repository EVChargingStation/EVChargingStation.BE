namespace EVChargingStation.Domain.Entities
{
    public class Report : BaseEntity
    {
        public string Type { get; set; }
        public string Data { get; set; }

        // Navigation property
        public Guid? UserId { get; set; }
        public User? User { get; set; }
    }
}
