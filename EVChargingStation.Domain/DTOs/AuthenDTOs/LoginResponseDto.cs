using BlindTreasure.Domain.DTOs.UserDTOs;

namespace BlindTreasure.Domain.DTOs.AuthenDTOs;

public class LoginResponseDto
{
    public string? AccessToken { get; set; }
    public string? RefreshToken { get; set; }

    public UserDto? User { get; set; }
}