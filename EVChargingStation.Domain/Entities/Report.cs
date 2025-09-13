namespace EVChargingStation.Domain.Entities
{
    public class Report : BaseEntity
    {
        public string Type { get; set; }
        public DateTime PeriodStart { get; set; }
        public DateTime PeriodEnd { get; set; }
        public string Data { get; set; }
        public DateTime GeneratedAt { get; set; }
    }
}
