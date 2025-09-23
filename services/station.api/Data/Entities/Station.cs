using System.ComponentModel.DataAnnotations;
using station.api.Data.Enums;
using Shared.Kernel.Entities;

namespace station.api.Data.Entities
{
    public class Station : BaseEntity
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;
        
        public Guid LocationId { get; set; }
        
        public StationStatus Status { get; set; } = StationStatus.Online;

        // Navigation Property
        public Location Location { get; set; } = null!;
        public ICollection<Connector> Connectors { get; set; } = new List<Connector>();
        
        // Cross-service references are removed - these become IDs only
        // public ICollection<StaffStation> StaffStations { get; set; } = new List<StaffStation>();
        // public ICollection<Recommendation> Recommendations { get; set; } = new List<Recommendation>();
        // public ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
    }
}