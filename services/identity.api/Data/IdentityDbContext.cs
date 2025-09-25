using identity.api.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace identity.api.Data;

public class IdentityDbContext : DbContext
{
    public IdentityDbContext()
    {
    }

    public IdentityDbContext(DbContextOptions<IdentityDbContext> options)
        : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Vehicle> Vehicles { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // User ↔ Vehicle (one-to-many)
        modelBuilder.Entity<Vehicle>()
            .HasOne(v => v.User)
            .WithMany(u => u.Vehicles)
            .HasForeignKey(v => v.UserId);
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

            var connectionString = configuration.GetConnectionString("IdentityConnection");

            if (!string.IsNullOrEmpty(connectionString)) optionsBuilder.UseNpgsql(connectionString);
        }
    }
}