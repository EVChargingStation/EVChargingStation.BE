namespace EVChargingStation.Domain.Entities
{
    public class Wallet : BaseEntity
    {
        public Guid UserId { get; set; }
        public Guid? PlanId { get; set; }
        public long Balance { get; set; }

        // Navigation property
        public User User { get; set; }
        public Plan Plan { get; set; }
        public ICollection<WalletTransaction> WalletTransactions { get; set; }
    }
}
