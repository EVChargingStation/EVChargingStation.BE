namespace EVChargingStation.Domain.Entities
{
    public class UserPlan : BaseEntity
    {
        public Guid UserId { get; set; }
        public Guid PlanId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        // Navigation property
        public User User { get; set; }
        public Plan Plan { get; set; }
    }
}
