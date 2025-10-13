using EVChargingStation.Domain.Entities;

namespace EVChargingStation.Infrastructure.Interfaces;

public interface IUnitOfWork : IDisposable
{
    IGenericRepository<User> Users { get; }

    IGenericRepository<Vehicle> Vehicles { get; }
    //IGenericRepository<Movie> Movies { get; }
    //IGenericRepository<ShowTime> ShowTimes { get; }
    //IGenericRepository<Promotion> Promotions { get; }
    //IGenericRepository<CinemaRoom> CinemaRooms { get; }
    //IGenericRepository<AuditLog> AuditLogs { get; }

    Task<int> SaveChangesAsync();
}