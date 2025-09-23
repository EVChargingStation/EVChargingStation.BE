using System.ComponentModel.DataAnnotations;

namespace staff.api.Data.Entities
{
    public class StaffStation : BaseEntity
    {
        [Required]
        public Guid StaffUserId { get; set; }
        
        [Required]
        public Guid StationId { get; set; }

        // Navigation properties - Cross-service references removed
        // StaffUser and Station are referenced by ID only
        // public User StaffUser { get; set; } = null!;
        // public Station Station { get; set; } = null!;
    }
}