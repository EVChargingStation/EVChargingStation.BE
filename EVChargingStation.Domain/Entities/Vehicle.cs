using EVChargingStation.Domain.Enums;

namespace EVChargingStation.Domain.Entities
{
    public class Vehicle : BaseEntity
    {
        public string Make { get; set; }            // Manufactory
        public string Model { get; set; }
        public int? Year { get; set; }
        public string LicensePlate { get; set; }
        public ConnectorType ConnectorType { get; set; }

        // Navigation property
        public Guid UserId { get; set; }
        public User User { get; set; }
    }
}
