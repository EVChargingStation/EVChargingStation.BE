using EVChargingStation.Domain.Enums;

namespace EVChargingStation.Domain.DTOs.UserDTOs;

[Serializable]
public class CurrentUserDto
{
    public string? FullName { get; set; }
    public string? Email { get; set; }
    public bool? Gender { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public string? AvatarUrl { get; set; }

    public string? PhoneNumber { get; set; }
    public RoleType? RoleName { get; set; }
}