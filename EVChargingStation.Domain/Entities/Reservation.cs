using EVChargingStation.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace EVChargingStation.Domain.Entities
{
    public class Reservation : BaseEntity
    {
        [Required]
        public int UserId { get; set; }
        
        [Required]
        public int StationId { get; set; }
        
        public int? ConnectorId { get; set; }
        
        public ConnectorType? PreferredConnectorType { get; set; }
        
        public decimal? MinPowerKw { get; set; }
        
        public PriceType PriceType { get; set; } = PriceType.Free;
        
        [Required]
        public DateTime StartTime { get; set; }
        
        public DateTime? EndTime { get; set; }
        
        public ReservationStatus Status { get; set; } = ReservationStatus.Pending;

        // Navigation properties
        public User User { get; set; } = null!;
        public Station Station { get; set; } = null!;
        public Connector? Connector { get; set; }
        public Session? Session { get; set; }
    }
}
