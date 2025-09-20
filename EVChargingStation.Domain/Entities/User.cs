using EVChargingStation.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace EVChargingStation.Domain.Entities
{
    public class User : BaseEntity
    {
        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; } = string.Empty;
        
        [Required]
        [MaxLength(50)]
        public string LastName { get; set; } = string.Empty;
        
        [Required]
        [MaxLength(255)]
        public string PasswordHash { get; set; } = string.Empty;
        
        public DateTime? DateOfBirth { get; set; }
        
        public Gender? Gender { get; set; }
        
        [Required]
        [MaxLength(255)]
        public string Email { get; set; } = string.Empty;
        
        [MaxLength(20)]
        public string? Phone { get; set; }
        
        public string? Address { get; set; }
        
        [Required]
        public RoleType Role { get; set; }
        
        public UserStatus Status { get; set; } = UserStatus.Active;

        // JWT Token (maintained for application functionality)
        [MaxLength(128)] 
        public string? RefreshToken { get; set; }
        
        public DateTime? RefreshTokenExpiryTime { get; set; }

        // Navigation properties
        public ICollection<Vehicle> Vehicles { get; set; } = new List<Vehicle>();
        public ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
        public ICollection<Session> Sessions { get; set; } = new List<Session>();
        public ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();
        public ICollection<Payment> Payments { get; set; } = new List<Payment>();
        public ICollection<UserPlan> UserPlans { get; set; } = new List<UserPlan>();
        public ICollection<Recommendation> Recommendations { get; set; } = new List<Recommendation>();
        public ICollection<StaffStation> StaffStations { get; set; } = new List<StaffStation>();
        public ICollection<Report> Reports { get; set; } = new List<Report>();
    }
}
