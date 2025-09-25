using System.ComponentModel.DataAnnotations;
using Shared.Kernel.Entities;

namespace staff.api.Data.Entities;

public class Report : BaseEntity
{
    [MaxLength(50)] public string? Type { get; set; }

    public DateTime? PeriodStart { get; set; }

    public DateTime? PeriodEnd { get; set; }

    public string? Data { get; set; } // JSON data

    public DateTime GeneratedAt { get; set; } = DateTime.UtcNow;

    // Navigation property - Cross-service references removed
    // User is referenced by ID only
    public Guid? UserId { get; set; }
    // public User? User { get; set; }
}