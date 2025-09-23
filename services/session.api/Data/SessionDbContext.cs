using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using session.api.Data.Entities;

namespace session.api.Data
{
    public class SessionDbContext : DbContext
    {
        public SessionDbContext() { }

        public SessionDbContext(DbContextOptions<SessionDbContext> options)
            : base(options)
        { }

        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Entities.Session> Sessions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Session ↔ Reservation (one-to-one, optional)
            modelBuilder.Entity<Entities.Session>()
                .HasOne(s => s.Reservation)
                .WithOne(r => r.Session)
                .HasForeignKey<Entities.Session>(s => s.ReservationId)
                .IsRequired(false);
        }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                // This is a fallback configuration in case the options aren't provided
                var configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", optional: false)
                    .AddJsonFile("appsettings.Development.json", optional: true)
                    .Build();

                var connectionString = configuration.GetConnectionString("SessionConnection");
                
                if (!string.IsNullOrEmpty(connectionString))
                {
                    optionsBuilder.UseNpgsql(connectionString);
                }
            }
        }
    }
}