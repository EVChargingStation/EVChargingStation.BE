using EVChargingStation.Domain;
using EVChargingStation.Domain.Entities;
using EVChargingStation.Infrastructure.Interfaces;

namespace EVChargingStation.Infrastructure;

public class UnitOfWork : IUnitOfWork
{
    private readonly EvChargingStationDbContext _dbContext;

    public UnitOfWork(EvChargingStationDbContext dbContext, IGenericRepository<User> users)
    {
        _dbContext = dbContext;
        Users = users;
    }


    public void Dispose()
    {
        _dbContext.Dispose();
    }

    public IGenericRepository<User> Users { get; }

    public async Task<int> SaveChangesAsync()
    {
        return await _dbContext.SaveChangesAsync();
    }
}