using Microsoft.EntityFrameworkCore;
using session.api.Domain.Entities;

namespace session.api.Domain;

public class SessionDbContext : DbContext
{
    public SessionDbContext()
    {
    }

    public SessionDbContext(DbContextOptions<SessionDbContext> options)
        : base(options)
    {
    }

    public DbSet<Reservation> Reservations { get; set; }
    public DbSet<Session> Sessions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Session ↔ Reservation (one-to-one, optional)
        modelBuilder.Entity<Session>()
            .HasOne(s => s.Reservation)
            .WithOne(r => r.Session)
            .HasForeignKey<Session>(s => s.ReservationId)
            .IsRequired(false);
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

            var connectionString = configuration.GetConnectionString("SessionConnection");

            if (!string.IsNullOrEmpty(connectionString)) optionsBuilder.UseNpgsql(connectionString);
        }
    }
}