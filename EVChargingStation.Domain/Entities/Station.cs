namespace EVChargingStation.Domain.Entities
{
    public class Station : BaseEntity
    {
        public string Name { get; set; }          
        public int LocationId { get; set; }    
        public string Status { get; set; } 

        // Navigation Property
        public Location Location { get; set; }
        public ICollection<Connector> Connectors { get; set; } 
        public ICollection<StaffStation> StaffStations { get; set; } 
        public ICollection<Recommendation> Recommendations { get; set; }
    }
}
