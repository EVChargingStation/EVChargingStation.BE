using EVChargingStation.Domain.Enums;

namespace EVChargingStation.Domain.Entities
{
    public class Plan : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public PriceType Type { get; set; }
        public decimal Price { get; set; }
        public decimal MaxDailyKwh { get; set; }

        // Navigation property
        public ICollection<UserPlan> UserPlans { get; set; }
    }
}
