using EVChargingStation.Application.Interfaces;
using EVChargingStation.Application.Utils;
using EVChargingStation.Domain.DTOs.AuthenDTOs;
using EVChargingStation.Domain.DTOs.UserDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EVChargingStation.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    private readonly IConfiguration _configuration;

    public AuthController(IAuthService authService, IConfiguration configuration)
    {
        _authService = authService;
        _configuration = configuration;
    }

    /// <summary>
    ///     Register a new EV Driver user
    /// </summary>
    /// <param name="registrationDto">User registration information</param>
    /// <returns>Newly created user information</returns>
    [HttpPost("register/driver")]
    [ProducesResponseType(typeof(ApiResult<UserDto>), 201)]
    [ProducesResponseType(typeof(ApiResult<>), 400)]
    [ProducesResponseType(typeof(ApiResult<>), 409)]
    public async Task<IActionResult> RegisterDriver([FromBody] UserRegistrationDto registrationDto)
    {
        try
        {
            var result = await _authService.RegisterDriverAsync(registrationDto);
            return Created(string.Empty, ApiResult<UserDto>.Success(result!, "201", "User registered successfully"));
        }
        catch (Exception ex)
        {
            return BadRequest(ApiResult<string>.Failure("400", ex.Message));
        }
    }

    /// <summary>
    ///     Login user with email and password
    /// </summary>
    /// <param name="loginDto">Login credentials</param>
    /// <returns>User information and authentication tokens</returns>
    [HttpPost("login")]
    [ProducesResponseType(typeof(ApiResult<LoginResponseDto>), 200)]
    [ProducesResponseType(typeof(ApiResult<>), 400)]
    [ProducesResponseType(typeof(ApiResult<>), 401)]
    [ProducesResponseType(typeof(ApiResult<>), 403)]
    public async Task<IActionResult> Login([FromBody] LoginRequestDto loginDto)
    {
        try
        {
            var result = await _authService.LoginAsync(loginDto, _configuration);
            return Ok(ApiResult<LoginResponseDto>.Success(result!, "200", "Login successful"));
        }
        catch (Exception ex)
        {
            return BadRequest(ApiResult<string>.Failure("400", ex.Message));
        }
    }

    /// <summary>
    ///     Logout current user
    /// </summary>
    /// <returns>Success or failure indication</returns>
    [HttpPost("logout")]
    [Authorize]
    [ProducesResponseType(typeof(ApiResult<bool>), 200)]
    [ProducesResponseType(typeof(ApiResult<>), 400)]
    [ProducesResponseType(typeof(ApiResult<>), 401)]
    public async Task<IActionResult> Logout()
    {
        try
        {
            // Get current user ID from claims
            if (!Guid.TryParse(User.FindFirst("uid")?.Value, out var userId))
                return Unauthorized(ApiResult<string>.Failure("401", "Invalid authentication token"));

            var result = await _authService.LogoutAsync(userId);
            return Ok(ApiResult<bool>.Success(result, "200", "Logged out successfully"));
        }
        catch (Exception ex)
        {
            return BadRequest(ApiResult<string>.Failure("400", ex.Message));
        }
    }
}