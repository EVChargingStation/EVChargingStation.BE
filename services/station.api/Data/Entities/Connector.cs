using System.ComponentModel.DataAnnotations;
using station.api.Data.Enums;

namespace station.api.Data.Entities
{
    public class Connector : BaseEntity
    {
        [Required]
        public Guid StationId { get; set; }
        
        [Required]
        public ConnectorType ConnectorType { get; set; }
        
        [Required]
        [Range(0, 1000)]
        public decimal PowerKw { get; set; }
        
        public ConnectorStatus Status { get; set; } = ConnectorStatus.Free;
        
        [Required]
        [Range(0, 10000)]
        public decimal PricePerKwh { get; set; }

        // Navigation property
        public Station Station { get; set; } = null!;
        
        // Cross-service references are removed - these become IDs only
        // public ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
        // public ICollection<Session> Sessions { get; set; } = new List<Session>();
        // public ICollection<Recommendation> Recommendations { get; set; } = new List<Recommendation>();
    }
}