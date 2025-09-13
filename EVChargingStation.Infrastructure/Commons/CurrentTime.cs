using EVChargingStation.Infrastructure.Interfaces;

namespace EVChargingStation.Infrastructure.Commons
{
    public class CurrentTime : ICurrentTime
    {
        public DateTime GetCurrentTime()
        {
            return DateTime.UtcNow; // Đảm bảo trả về thời gian UTC
        }
    }
}
