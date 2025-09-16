using EVChargingStation.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace EVChargingStation.Domain.Entities
{
    public class User : BaseEntity
    {
        public string FullName { get; set; }
        public string PasswordHash { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public Gender Sex { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public RoleType Role { get; set; }

        // JWT Token
        [MaxLength(128)] public string? RefreshToken { get; set; }
        [MaxLength(128)] public DateTime? RefreshTokenExpiryTime { get; set; }

        public UserStatus UserStatus { get; set; }


        // Navigation
        public ICollection<Vehicle> Vehicles { get; set; }
        public ICollection<Reservation> Reservations { get; set; }
        public ICollection<Session> Sessions { get; set; }
        public ICollection<Invoice> Invoices { get; set; }
        public ICollection<Payment> Payments { get; set; }
        public ICollection<UserPlan> UserPlans { get; set; }
        public ICollection<Recommendation> Recommendations { get; set; }
        public ICollection<StaffStation> StaffStations { get; set; }
        public ICollection<Report> Reports { get; set; }
    }
}
