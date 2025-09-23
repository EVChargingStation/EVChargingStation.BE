using System.ComponentModel.DataAnnotations;
using session.api.Data.Enums;

namespace session.api.Data.Entities
{
    public class Reservation : BaseEntity
    {
        [Required]
        public Guid UserId { get; set; }
        
        [Required]
        public Guid StationId { get; set; }
        
        public Guid? ConnectorId { get; set; }
        
        public ConnectorType? PreferredConnectorType { get; set; }
        
        public decimal? MinPowerKw { get; set; }
        
        public PriceType PriceType { get; set; } = PriceType.Free;
        
        [Required]
        public DateTime StartTime { get; set; }
        
        public DateTime? EndTime { get; set; }
        
        public ReservationStatus Status { get; set; } = ReservationStatus.Pending;

        // Navigation properties - Cross-service references removed
        // User, Station, Connector are referenced by ID only
        // public User User { get; set; } = null!;
        // public Station Station { get; set; } = null!;
        // public Connector? Connector { get; set; }
        
        // Navigation within same bounded context
        public Session? Session { get; set; }
    }
}