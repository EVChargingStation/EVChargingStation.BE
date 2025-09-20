using System.ComponentModel.DataAnnotations;

namespace EVChargingStation.Domain.Entities
{
    public class Report : BaseEntity
    {
        [MaxLength(50)]
        public string? Type { get; set; }
        
        public DateTime? PeriodStart { get; set; }
        
        public DateTime? PeriodEnd { get; set; }
        
        public string? Data { get; set; } // JSON data
        
        public DateTime GeneratedAt { get; set; } = DateTime.UtcNow;

        // Navigation property
        public int? UserId { get; set; }
        public User? User { get; set; }
    }
}
