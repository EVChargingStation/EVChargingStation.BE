using EVChargingStation.Domain.DTOs.UserDTOs;

namespace EVChargingStation.Domain.DTOs.AuthenDTOs;

public class LoginResponseDto
{
    public string? AccessToken { get; set; }
    public string? RefreshToken { get; set; }

    public UserDto? User { get; set; }
}