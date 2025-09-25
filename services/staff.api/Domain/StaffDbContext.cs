using Microsoft.EntityFrameworkCore;
using staff.api.Data.Entities;

namespace staff.api.Data;

public class StaffDbContext : DbContext
{
    public StaffDbContext()
    {
    }

    public StaffDbContext(DbContextOptions<StaffDbContext> options)
        : base(options)
    {
    }

    public DbSet<StaffStation> StaffStations { get; set; }
    public DbSet<Report> Reports { get; set; }
    public DbSet<Recommendation> Recommendations { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // No complex relationships within this bounded context
        // All cross-service relationships are handled via IDs
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            // This is a fallback configuration in case the options aren't provided
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", false)
                .AddJsonFile("appsettings.Development.json", true)
                .Build();

            var connectionString = configuration.GetConnectionString("StaffConnection");

            if (!string.IsNullOrEmpty(connectionString)) optionsBuilder.UseNpgsql(connectionString);
        }
    }
}