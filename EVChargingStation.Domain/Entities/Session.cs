using EVChargingStation.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace EVChargingStation.Domain.Entities
{
    public class Session : BaseEntity
    {
        [Required]
        public int ConnectorId { get; set; }
        
        [Required]
        public int UserId { get; set; }
        
        public int? ReservationId { get; set; }
        
        [Required]
        public DateTime StartTime { get; set; }
        
        public DateTime? EndTime { get; set; }
        
        public SessionStatus Status { get; set; } = SessionStatus.Running;
        
        // State of Charge (SoC) at the start and end of the session
        public decimal? SocStart { get; set; }
        public decimal? SocEnd { get; set; }
        
        // Energy consumed in kWh during the session
        public decimal? EnergyKwh { get; set; }
        
        public decimal? Cost { get; set; }
        
        public int? InvoiceId { get; set; }

        // Navigation properties
        public Connector Connector { get; set; } = null!;
        public User User { get; set; } = null!;
        public Reservation? Reservation { get; set; }
        public Invoice? Invoice { get; set; }
        public ICollection<Payment> Payments { get; set; } = new List<Payment>();
    }
}
