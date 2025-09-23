using System.ComponentModel.DataAnnotations;

namespace billing.api.Data.Entities
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

        // Navigation properties - Cross-service references removed
        // User is referenced by ID only
        // public User User { get; set; } = null!;
        
        // Navigation within same bounded context
        public Plan Plan { get; set; } = null!;
    }
}