using System.ComponentModel.DataAnnotations;
using EVChargingStation.Domain.Enums;

namespace EVChargingStation.Domain.Entities;

public class Connector : BaseEntity
{
    [Required] public Guid StationId { get; set; }

    [Required] public ConnectorType ConnectorType { get; set; }

    [Required] [Range(0, 1000)] public decimal PowerKw { get; set; }

    public ConnectorStatus Status { get; set; } = ConnectorStatus.Free;

    [Required] [Range(0, 10000)] public decimal PricePerKwh { get; set; }

    // Navigation property
    public Station Station { get; set; } = null!;
    public ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
    public ICollection<Session> Sessions { get; set; } = new List<Session>();
    public ICollection<Recommendation> Recommendations { get; set; } = new List<Recommendation>();
}