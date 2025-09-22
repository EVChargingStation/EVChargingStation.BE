using System.ComponentModel.DataAnnotations;

namespace EVChargingStation.Domain.Entities
{
    public class UserPlan : BaseEntity
    {
        [Required]
        public Guid UserId { get; set; }
        
        [Required]
        public Guid PlanId { get; set; }
        
        [Required]
        public DateTime StartDate { get; set; }
        
        public DateTime? EndDate { get; set; }

        // Navigation properties
        public User User { get; set; } = null!;
        public Plan Plan { get; set; } = null!;
    }
}
