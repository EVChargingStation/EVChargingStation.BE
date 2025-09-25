using System.ComponentModel.DataAnnotations;
using session.api.Data.Enums;
using Shared.Kernel.Entities;

namespace session.api.Data.Entities;

public class Session : BaseEntity
{
    [Required] public Guid ConnectorId { get; set; }

    [Required] public Guid UserId { get; set; }

    public Guid? ReservationId { get; set; }

    [Required] public DateTime StartTime { get; set; }

    public DateTime? EndTime { get; set; }

    public SessionStatus Status { get; set; } = SessionStatus.Running;

    // State of Charge (SoC) at the start and end of the session
    public decimal? SocStart { get; set; }
    public decimal? SocEnd { get; set; }

    // Energy consumed in kWh during the session
    public decimal? EnergyKwh { get; set; }

    public decimal? Cost { get; set; }

    public Guid? InvoiceId { get; set; }

    // Navigation properties - Cross-service references removed
    // Connector, User, Invoice are referenced by ID only
    // public Connector Connector { get; set; } = null!;
    // public User User { get; set; } = null!;
    // public Invoice? Invoice { get; set; }
    // public ICollection<Payment> Payments { get; set; } = new List<Payment>();

    // Navigation within same bounded context
    public Reservation? Reservation { get; set; }
}