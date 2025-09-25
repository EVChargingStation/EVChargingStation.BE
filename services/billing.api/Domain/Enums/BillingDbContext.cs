using billing.api.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace billing.api.Domain.Enums;

public class BillingDbContext : DbContext
{
    public BillingDbContext()
    {
    }

    public BillingDbContext(DbContextOptions<BillingDbContext> options)
        : base(options)
    {
    }

    public DbSet<Invoice> Invoices { get; set; }
    public DbSet<Payment> Payments { get; set; }
    public DbSet<Plan> Plans { get; set; }
    public DbSet<UserPlan> UserPlans { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Payment ↔ Invoice (many-to-one, optional)
        modelBuilder.Entity<Payment>()
            .HasOne(p => p.Invoice)
            .WithMany(i => i.Payments)
            .HasForeignKey(p => p.InvoiceId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.SetNull);

        // UserPlan ↔ Plan (many-to-one)
        modelBuilder.Entity<UserPlan>()
            .HasOne(up => up.Plan)
            .WithMany(p => p.UserPlans)
            .HasForeignKey(up => up.PlanId);
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

            var connectionString = configuration.GetConnectionString("BillingConnection");

            if (!string.IsNullOrEmpty(connectionString)) optionsBuilder.UseNpgsql(connectionString);
        }
    }
}