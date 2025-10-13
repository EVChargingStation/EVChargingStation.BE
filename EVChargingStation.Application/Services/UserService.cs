using System.Linq.Expressions;
using EVChargingStation.Application.Interfaces;
using EVChargingStation.Application.Interfaces.Commons;
using EVChargingStation.Application.Utils;
using EVChargingStation.Domain.DTOs.UserDTOs;
using EVChargingStation.Domain.Entities;
using EVChargingStation.Domain.Enums;
using EVChargingStation.Infrastructure.Interfaces;

namespace EVChargingStation.Application.Services;

public class UserService : IUserService
{
    private readonly ILoggerService _loggerService;
    private readonly IUnitOfWork _unitOfWork;

    public UserService(IUnitOfWork unitOfWork, ILoggerService loggerService)
    {
        _unitOfWork = unitOfWork;
        _loggerService = loggerService;
    }

    #region Private Helper Methods

    private static UserDto ToUserDto(User user)
    {
        return new UserDto
        {
            UserId = user.Id,
            FullName = $"{user.FirstName} {user.LastName}".Trim(),
            Email = user.Email,
            DateOfBirth = user.DateOfBirth,
            Gender = user.Gender == Gender.Male,
            Status = user.Status,
            PhoneNumber = user.Phone,
            RoleName = user.Role,
            CreatedAt = user.CreatedAt
        };
    }

    #endregion

    #region User Management

    /// <summary>
    ///     Gets a user by their ID
    /// </summary>
    public async Task<UserDto> GetUserByIdAsync(Guid userId)
    {
        var user = await _unitOfWork.Users.GetByIdAsync(userId);
        if (user == null)
            throw ErrorHelper.NotFound(ErrorMessages.AccountNotFound);

        return ToUserDto(user);
    }

    /// <summary>
    ///     Gets all users with optional filtering
    /// </summary>
    public async Task<List<UserDto>> GetAllUsersAsync(string? searchTerm = null, RoleType? roleFilter = null)
    {
        Expression<Func<User, bool>> filter = user => !user.IsDeleted;

        // Apply search filter if provided
        if (!string.IsNullOrWhiteSpace(searchTerm))
        {
            var search = searchTerm.Trim().ToLower();
            filter = user => !user.IsDeleted &&
                             (user.Email.ToLower().Contains(search) ||
                              user.FirstName.ToLower().Contains(search) ||
                              user.LastName.ToLower().Contains(search) ||
                              (user.Phone != null && user.Phone.Contains(search)));
        }

        // Apply role filter if provided
        if (roleFilter.HasValue)
        {
            var roleType = roleFilter.Value;
            filter = user => !user.IsDeleted && user.Role == roleType;
        }

        var users = await _unitOfWork.Users.GetAllAsync(filter);
        return users.Select(ToUserDto).ToList();
    }

    /// <summary>
    ///     Updates a user's profile information
    /// </summary>
    public async Task<UserDto> UpdateUserProfileAsync(Guid userId, UserDto updateDto)
    {
        var user = await _unitOfWork.Users.GetByIdAsync(userId);
        if (user == null)
            throw ErrorHelper.NotFound(ErrorMessages.AccountNotFound);

        // Update user fields with new values
        if (!string.IsNullOrWhiteSpace(updateDto.FullName))
        {
            var nameParts = updateDto.FullName.Trim().Split(' ', 2);
            user.FirstName = nameParts[0];
            user.LastName = nameParts.Length > 1 ? nameParts[1] : "";
        }

        if (updateDto.DateOfBirth.HasValue)
            user.DateOfBirth = updateDto.DateOfBirth;

        if (updateDto.Gender.HasValue)
            user.Gender = updateDto.Gender.Value ? Gender.Male : Gender.Female;

        if (!string.IsNullOrWhiteSpace(updateDto.PhoneNumber))
            user.Phone = updateDto.PhoneNumber;

        // Update status if admin is making the change and status is provided
        if (updateDto.Status.HasValue)
            user.Status = updateDto.Status.Value;

        await _unitOfWork.Users.Update(user);
        await _unitOfWork.SaveChangesAsync();

        _loggerService.Info($"User {userId} profile updated");

        return ToUserDto(user);
    }

    /// <summary>
    ///     Soft deletes a user (for admin use)
    /// </summary>
    public async Task<bool> DeleteUserAsync(Guid userId)
    {
        var user = await _unitOfWork.Users.GetByIdAsync(userId);
        if (user == null)
            throw ErrorHelper.NotFound(ErrorMessages.AccountNotFound);

        // Soft delete
        await _unitOfWork.Users.SoftRemove(user);
        await _unitOfWork.SaveChangesAsync();

        _loggerService.Info($"User {userId} marked as deleted");

        return true;
    }

    #endregion
}