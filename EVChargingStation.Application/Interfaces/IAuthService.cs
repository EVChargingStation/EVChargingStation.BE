using BlindTreasure.Domain.DTOs.AuthenDTOs;
using BlindTreasure.Domain.DTOs.UserDTOs;
using Microsoft.Extensions.Configuration;

namespace EVChargingStation.Application.Interfaces;

public interface IAuthService
{
    Task<UserDto?> RegisterDriverAsync(UserRegistrationDto registrationDto);
    Task<LoginResponseDto?> LoginAsync(LoginRequestDto loginDto, IConfiguration configuration);
    Task<bool> LogoutAsync(Guid userId);
}