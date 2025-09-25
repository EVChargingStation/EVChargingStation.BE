using Microsoft.EntityFrameworkCore;
using station.api.Domain.Entities;

namespace station.api.Domain;

public class StationDbContext : DbContext
{
    public StationDbContext()
    {
    }

    public StationDbContext(DbContextOptions<StationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Location> Locations { get; set; }
    public DbSet<Station> Stations { get; set; }
    public DbSet<Connector> Connectors { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Location ↔ Station (one-to-many)
        modelBuilder.Entity<Station>()
            .HasOne(s => s.Location)
            .WithMany(l => l.Stations)
            .HasForeignKey(s => s.LocationId);

        // Station ↔ Connector (one-to-many)
        modelBuilder.Entity<Connector>()
            .HasOne(c => c.Station)
            .WithMany(s => s.Connectors)
            .HasForeignKey(c => c.StationId);
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

            var connectionString = configuration.GetConnectionString("StationConnection");

            if (!string.IsNullOrEmpty(connectionString)) optionsBuilder.UseNpgsql(connectionString);
        }
    }
}