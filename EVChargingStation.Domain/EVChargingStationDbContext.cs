using EVChargingStation.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EVChargingStation.Domain
{
    public class EVChargingStationDbContext : DbContext
    {
        public EVChargingStationDbContext() { }

        public EVChargingStationDbContext(DbContextOptions<EVChargingStationDbContext> options)
            : base(options)
        { }

        public DbSet<User> Users { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<Station> Stations { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Connector> Connectors { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Plan> Plans { get; set; }
        public DbSet<UserPlan> UserPlans { get; set; }
        public DbSet<Wallet> Wallets { get; set; }
        public DbSet<WalletTransaction> WalletTransactions { get; set; }
        public DbSet<StaffStation> StaffStations { get; set; }
        public DbSet<Report> Reports { get; set; }
        public DbSet<Recommendation> Recommendations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Location ↔ Station (one-to-many)
            modelBuilder.Entity<Station>()
                .HasOne(s => s.Location)  
                .WithMany(l => l.Stations)
                .HasForeignKey(s => s.LocationId);

            // User ↔ Vehicle (one-to-many)
            modelBuilder.Entity<Vehicle>()
                .HasOne(v => v.User)
                .WithMany(u => u.Vehicles)
                .HasForeignKey(v => v.UserId);

            // Station ↔ Connector (one-to-many)
            modelBuilder.Entity<Connector>()
                .HasOne(c => c.Station)
                .WithMany(s => s.Connectors)
                .HasForeignKey(c => c.StationId);

            // Reservation ↔ User (many-to-one)
            modelBuilder.Entity<Reservation>()
                .HasOne(r => r.User)
                .WithMany(u => u.Reservations)
                .HasForeignKey(r => r.UserId);

            // Reservation ↔ Connector (many-to-one)
            modelBuilder.Entity<Reservation>()
                .HasOne(r => r.Connector)
                .WithMany(c => c.Reservations)
                .HasForeignKey(r => r.ConnectorId);

            // Session ↔ Reservation (one-to-one)
            modelBuilder.Entity<Session>()
                .HasOne(s => s.Reservation)
                .WithOne(r => r.Session)
                .HasForeignKey<Session>(s => s.ReservationId);

            // Session ↔ Connector (many-to-one)
            modelBuilder.Entity<Session>()
                .HasOne(s => s.Connector)
                .WithMany(c => c.Sessions)
                .HasForeignKey(s => s.ConnectorId);

            // Session ↔ User (many-to-one)
            modelBuilder.Entity<Session>()
                .HasOne(s => s.User)
                .WithMany(u => u.Sessions)
                .HasForeignKey(s => s.UserId);

            // Invoice ↔ User (many-to-one)
            modelBuilder.Entity<Invoice>()
                .HasOne(i => i.User)
                .WithMany(u => u.Invoices)
                .HasForeignKey(i => i.UserId);

            // Payment ↔ Invoice (many-to-one)
            modelBuilder.Entity<Payment>()
                .HasOne(p => p.Invoice)
                .WithMany(i => i.Payments)
                .HasForeignKey(p => p.InvoiceId);

            // Payment ↔ Session (optional)
            modelBuilder.Entity<Payment>()
                .HasOne(p => p.Session)
                .WithMany(s => s.Payments)
                .HasForeignKey(p => p.SessionId)
                .OnDelete(DeleteBehavior.SetNull);

            // UserPlan ↔ User (many-to-one)
            modelBuilder.Entity<UserPlan>()
                .HasOne(up => up.User)
                .WithMany(u => u.UserPlans)
                .HasForeignKey(up => up.UserId);

            // UserPlan ↔ Plan (many-to-one)
            modelBuilder.Entity<UserPlan>()
                .HasOne(up => up.Plan)
                .WithMany(p => p.UserPlans)
                .HasForeignKey(up => up.PlanId);

            // Wallet ↔ User (one-to-one)
            modelBuilder.Entity<Wallet>()
                .HasOne(w => w.User)
                .WithOne(u => u.Wallet)
                .HasForeignKey<Wallet>(w => w.UserId);

            // WalletTransaction ↔ Wallet (many-to-one)
            modelBuilder.Entity<WalletTransaction>()
                .HasOne(wt => wt.Wallet)
                .WithMany(w => w.WalletTransactions)
                .HasForeignKey(wt => wt.WalletId);

            // WalletTransaction ↔ Payment (optional)
            modelBuilder.Entity<WalletTransaction>()
                .HasOne(wt => wt.Payment)
                .WithMany(p => p.WalletTransactions)
                .HasForeignKey(wt => wt.PaymentId)
                .OnDelete(DeleteBehavior.SetNull);

            // StaffStation ↔ User (many-to-one)
            modelBuilder.Entity<StaffStation>()
                .HasOne(ss => ss.StaffUser)
                .WithMany(u => u.StaffStations)
                .HasForeignKey(ss => ss.StaffUserId);

            // StaffStation ↔ Station (many-to-one)
            modelBuilder.Entity<StaffStation>()
                .HasOne(ss => ss.Station)
                .WithMany(s => s.StaffStations)
                .HasForeignKey(ss => ss.StationId);

            // Recommendation ↔ User (many-to-one)
            modelBuilder.Entity<Recommendation>()
                .HasOne(r => r.User)
                .WithMany(u => u.Recommendations)
                .HasForeignKey(r => r.UserId);

            // Recommendation ↔ Station (many-to-one)
            modelBuilder.Entity<Recommendation>()
                .HasOne(r => r.Station)
                .WithMany(s => s.Recommendations)
                .HasForeignKey(r => r.StationId);

            // Recommendation ↔ Connector (optional, many-to-one)
            modelBuilder.Entity<Recommendation>()
                .HasOne(r => r.Connector)
                .WithMany(c => c.Recommendations)
                .HasForeignKey(r => r.ConnectorId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
