using EVChargingStation.Application.Interfaces;
using EVChargingStation.Application.Interfaces.Commons;
using EVChargingStation.Application.Utils;
using EVChargingStation.Domain.DTOs.AuthenDTOs;
using EVChargingStation.Domain.DTOs.UserDTOs;
using EVChargingStation.Domain.Entities;
using EVChargingStation.Domain.Enums;
using EVChargingStation.Infrastructure.Interfaces;
using Microsoft.Extensions.Configuration;

namespace EVChargingStation.Application.Services;

public class AuthService : IAuthService
{
    private readonly ILoggerService _logger;
    private readonly IUnitOfWork _unitOfWork;


    public AuthService(ILoggerService logger, IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
    }

    #region Authen

    public async Task<UserDto?> RegisterDriverAsync(UserRegistrationDto registrationDto)
    {
        if (await UserExistsAsync(registrationDto.Email))
            throw ErrorHelper.Conflict(ErrorMessages.AccountEmailAlreadyRegistered);

        var hashedPassword = new PasswordHasher().HashPassword(registrationDto.Password);

        var user = new User
        {
            Email = registrationDto.Email,
            PasswordHash = hashedPassword,
            FirstName = registrationDto.FullName,
            Phone = registrationDto.PhoneNumber,
            DateOfBirth = registrationDto.DateOfBirth,
            Status = UserStatus.Pending,
            Role = RoleType.Driver
        };

        await _unitOfWork.Users.AddAsync(user);
        await _unitOfWork.SaveChangesAsync();

        return ToUserDto(user);
    }

    public async Task<LoginResponseDto?> LoginAsync(LoginRequestDto loginDto, IConfiguration configuration)
    {
        var user = await GetUserByEmailAsync(loginDto.Email!);
        // GetUserByEmailAsync already throws an error if user is null, so this check is redundant
        // but kept for clarity
        if (user == null)
            throw ErrorHelper.NotFound(ErrorMessages.AccountNotFound);

        // Nếu không phải login Google thì verify password
        if (!loginDto.IsLoginGoole.GetValueOrDefault())
            if (!new PasswordHasher().VerifyPassword(loginDto.Password!, user.PasswordHash))
                throw ErrorHelper.Unauthorized(ErrorMessages.AccountWrongPassword);

        // Check account status
        if (user.Status != UserStatus.Active)
            throw ErrorHelper.Forbidden(ErrorMessages.AccountNotVerified);

        // Generate JWT & RefreshToken
        var accessToken = JwtUtils.GenerateJwtToken(
            user.Id,
            user.Email,
            user.Role.ToString(),
            configuration,
            TimeSpan.FromMinutes(30)
        );

        var refreshToken = Guid.NewGuid().ToString();
        user.RefreshToken = refreshToken;
        user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);

        await _unitOfWork.Users.Update(user);
        await _unitOfWork.SaveChangesAsync();

        return new LoginResponseDto
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken,
            User = ToUserDto(user)
        };
    }

    public async Task<bool> LogoutAsync(Guid userId)
    {
        var user = await GetUserById(userId);
        if (user == null)
            throw ErrorHelper.NotFound(ErrorMessages.AccountNotFound);

        if (user.IsDeleted || user.Status == UserStatus.Banned)
            throw ErrorHelper.Forbidden(ErrorMessages.AccountSuspendedOrBan);

        if (string.IsNullOrEmpty(user.RefreshToken))
            throw ErrorHelper.BadRequest(ErrorMessages.AccountAccesstokenInvalid);

        // Xóa token trong DB
        user.RefreshToken = null;
        user.RefreshTokenExpiryTime = null;
        await _unitOfWork.Users.Update(user);
        await _unitOfWork.SaveChangesAsync();

        return true;
    }

    #endregion


    #region PRIVATE HELPER METHODS

    /// <summary>
    ///     Checks if a user exists in DB.
    /// </summary>
    private async Task<bool> UserExistsAsync(string email)
    {
        var existingUser = await _unitOfWork.Users.FirstOrDefaultAsync(u => u.Email == email);
        return existingUser != null;
    }

    /// <summary>
    ///     Gets a user by email.
    /// </summary>
    private async Task<User?> GetUserByEmailAsync(string email)
    {
        var user = await _unitOfWork.Users.FirstOrDefaultAsync(u => u.Email == email && !u.IsDeleted);

        if (user == null)
            throw ErrorHelper.NotFound(ErrorMessages.AccountNotFound);

        return user;
    }

    /// <summary>
    ///     Gets a user by id
    /// </summary>
    private async Task<User?> GetUserById(Guid id)
    {
        return await _unitOfWork.Users.FirstOrDefaultAsync(u => u.Id == id);
    }

    private static UserDto ToUserDto(User user)
    {
        return new UserDto
        {
            UserId = user.Id,
            FullName = user.FirstName,
            Email = user.Email,
            DateOfBirth = user.DateOfBirth,
            Status = user.Status,
            PhoneNumber = user.Phone,
            RoleName = user.Role,
            CreatedAt = user.CreatedAt
        };
    }

    #endregion
}