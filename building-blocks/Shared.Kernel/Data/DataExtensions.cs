using Microsoft.Extensions.DependencyInjection;

namespace Shared.Kernel.Data;

public static class DataExtensions
{
    public static IServiceCollection AddGenericRepositoryAndUnitOfWork(this IServiceCollection services)
    {
        // Register the generic repository and unit of work
        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }
}