using EVChargingStation.Domain;
using EVChargingStation.Infrastructure.Interfaces;

namespace EVChargingStation.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly EVChargingStationDbContext _dbContext;

        public UnitOfWork(EVChargingStationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        //public IGenericRepository<User> Users { get; }
        //public IGenericRepository<OtpStorage> OtpStorages { get; }
        //public IGenericRepository<Movie> Movies { get; }
        //public IGenericRepository<ShowTime> ShowTimes { get; }
        //public IGenericRepository<Promotion> Promotions { get; }
        //public IGenericRepository<CinemaRoom> CinemaRooms { get; }
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
