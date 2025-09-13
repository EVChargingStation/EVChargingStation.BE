namespace EVChargingStation.Domain.Entities
{
    public class Station : BaseEntity
    {
        public string Name { get; set; }
        public string Address { get; set; }

        // Kinh độ, vĩ độ
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public string Status { get; set; }

        // Navigation property
        public ICollection<Connector> Connectors { get; set; }
        public ICollection<StaffStation> StaffStations { get; set; }
        public ICollection<Recommendation> Recommendations { get; set; }
    }
}
