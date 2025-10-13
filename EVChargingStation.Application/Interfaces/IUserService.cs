using EVChargingStation.Domain.DTOs.UserDTOs;
using EVChargingStation.Domain.Enums;

namespace EVChargingStation.Application.Interfaces;

public interface IUserService
{
    // User Management
    Task<UserDto> GetUserByIdAsync(Guid userId);
    Task<List<UserDto>> GetAllUsersAsync(string? searchTerm = null, RoleType? roleFilter = null);
    Task<UserDto> UpdateUserProfileAsync(Guid userId, UserDto updateDto);
    Task<bool> DeleteUserAsync(Guid userId);
}