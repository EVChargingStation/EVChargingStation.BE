namespace EVChargingStation.Domain.Entities
{
    public class Recommendation : BaseEntity
    {
        public Guid UserId { get; set; }
        public Guid StationId { get; set; }
        public Guid? ConnectorId { get; set; }
        public DateTime SuggestedAt { get; set; }

        /// <summary>
        /// ConfidenceScore base on: 
        /// - The vehicle's compatibility with the connector type at the station.
        /// - The distance between the user and the station.
        /// - Historical data or user behavior patterns that suggest certain stations or connectors are frequently used successfully.
        /// </summary>
        public decimal ConfidenceScore { get; set; }


        // Navigation property
        public User User { get; set; }
        public Station Station { get; set; }
        public Connector Connector { get; set; }
    }
}
