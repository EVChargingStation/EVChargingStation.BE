using EVChargingStation.Domain.Enums;

namespace BlindTreasure.Domain.DTOs.UserDTOs;

public class UserDto
{
    public Guid UserId { get; set; }
    public string? FullName { get; set; }
    public string? Email { get; set; }
    public string? AvatarUrl { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public bool? Gender { get; set; }

    public UserStatus? Status { get; set; }

    public string? PhoneNumber { get; set; }
    public RoleType? RoleName { get; set; }
    public string? Reason { get; set; }
    public DateTime CreatedAt { get; set; }
    public Guid? SellerId { get; set; }
}