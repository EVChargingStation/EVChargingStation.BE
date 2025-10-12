namespace BlindTreasure.Domain.DTOs.AuthenDTOs;

public class TokenRefreshRequestDto
{
    public required string RefreshToken { get; set; }
}