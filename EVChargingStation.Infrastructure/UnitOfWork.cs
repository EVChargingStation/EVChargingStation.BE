using EVChargingStation.Domain;
using EVChargingStation.Infrastructure.Interfaces;

namespace EVChargingStation.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly EvChargingStationDbContext _dbContext;

        public UnitOfWork(EvChargingStationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }
    }
}
