namespace EVChargingStation.Domain.Entities
{
    public class StaffStation : BaseEntity
    {
        public Guid StaffUserId { get; set; }
        public Guid StationId { get; set; }

        // Navigation property
        public User StaffUser { get; set; }
        public Station Station { get; set; }
    }
}
