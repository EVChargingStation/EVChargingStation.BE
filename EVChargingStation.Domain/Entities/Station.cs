using System.ComponentModel.DataAnnotations;
using EVChargingStation.Domain.Enums;

namespace EVChargingStation.Domain.Entities;

public class Station : BaseEntity
{
    [Required] [MaxLength(100)] public string Name { get; set; } = string.Empty;

    public Guid LocationId { get; set; }

    public StationStatus Status { get; set; } = StationStatus.Online;

    // Navigation Property
    public Location Location { get; set; } = null!;
    public ICollection<Connector> Connectors { get; set; } = new List<Connector>();
    public ICollection<StaffStation> StaffStations { get; set; } = new List<StaffStation>();
    public ICollection<Recommendation> Recommendations { get; set; } = new List<Recommendation>();
    public ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
}