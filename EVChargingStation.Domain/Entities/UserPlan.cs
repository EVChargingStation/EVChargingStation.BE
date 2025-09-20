using System.ComponentModel.DataAnnotations;

namespace EVChargingStation.Domain.Entities
{
    public class UserPlan : BaseEntity
    {
        [Required]
        public int UserId { get; set; }
        
        [Required]
        public int PlanId { get; set; }
        
        [Required]
        public DateTime StartDate { get; set; }
        
        public DateTime? EndDate { get; set; }

        // Navigation properties
        public User User { get; set; } = null!;
        public Plan Plan { get; set; } = null!;
    }
}
