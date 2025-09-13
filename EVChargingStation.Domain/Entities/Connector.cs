using EVChargingStation.Domain.Enums;

namespace EVChargingStation.Domain.Entities
{
    public class Connector : BaseEntity
    {
        public Guid StationId { get; set; }
        public ConnectorType ConnectorType { get; set; }
        public decimal PowerKw { get; set; }
        public string Status { get; set; }
        public decimal PricePerKwh { get; set; }

        // Navigation property
        public Station Station { get; set; }
        public ICollection<Reservation> Reservations { get; set; }
        public ICollection<Session> Sessions { get; set; }
        public ICollection<Recommendation> Recommendations { get; set; }
    }
}
