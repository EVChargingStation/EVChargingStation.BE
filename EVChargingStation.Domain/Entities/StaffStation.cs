using System.ComponentModel.DataAnnotations;

namespace EVChargingStation.Domain.Entities
{
    public class StaffStation : BaseEntity
    {
        [Required]
        public Guid StaffUserId { get; set; }
        
        [Required]
        public Guid StationId { get; set; }

        // Navigation properties
        public User StaffUser { get; set; } = null!;
        public Station Station { get; set; } = null!;
    }
}
