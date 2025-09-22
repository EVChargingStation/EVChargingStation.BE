using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace EVChargingStation.Domain
{
    public class EVChargingStationDbContextFactory : IDesignTimeDbContextFactory<EvChargingStationDbContext>
    {
        public EvChargingStationDbContext CreateDbContext(string[] args)
        {
            // For migrations and other design-time operations
            var optionsBuilder = new DbContextOptionsBuilder<EvChargingStationDbContext>();
            
            // Set up configuration to read connection string
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false)
                .AddJsonFile("appsettings.Development.json", optional: true)
                .Build();

            // Get the connection string from configuration
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            
            // For PostgreSQL: Npgsql.EntityFrameworkCore.PostgreSQL
            optionsBuilder.UseNpgsql(connectionString, 
                npgsqlOptionsAction => npgsqlOptionsAction.MigrationsAssembly(typeof(EvChargingStationDbContext).Assembly.FullName));
            
            return new EvChargingStationDbContext(optionsBuilder.Options);
        }
    }
}
