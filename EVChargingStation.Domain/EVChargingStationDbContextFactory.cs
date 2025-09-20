using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace EVChargingStation.Domain
{
    public class EVChargingStationDbContextFactory : IDesignTimeDbContextFactory<EVChargingStationDbContext>
    {
        public EVChargingStationDbContext CreateDbContext(string[] args)
        {
            // For migrations and other design-time operations
            var optionsBuilder = new DbContextOptionsBuilder<EVChargingStationDbContext>();
            
            // Set up configuration to read connection string
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false)
                .AddJsonFile("appsettings.Development.json", optional: true)
                .Build();

            // Get the connection string from configuration
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            
            // Note: You need to uncomment one of the following options and 
            // add the corresponding NuGet package to your project:
            
            // For SQL Server: Microsoft.EntityFrameworkCore.SqlServer
            // optionsBuilder.UseSqlServer(connectionString);
            
            // For PostgreSQL: Npgsql.EntityFrameworkCore.PostgreSQL
            // optionsBuilder.UseNpgsql(connectionString);
            
            // For MySQL: Pomelo.EntityFrameworkCore.MySql
            // optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
            
            // For in-memory (testing): Microsoft.EntityFrameworkCore.InMemory
            // optionsBuilder.UseInMemoryDatabase("EVChargingStationDb");

            // For demonstration purposes, we'll create a temporary context without a provider
            // This will not work for migrations but prevents compilation errors
            return new EVChargingStationDbContext(optionsBuilder.Options);
        }
    }
}
