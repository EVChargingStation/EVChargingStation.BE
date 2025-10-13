using EVChargingStation.Application.Interfaces.Commons;
using EVChargingStation.Application.Utils;
using EVChargingStation.Domain;
using EVChargingStation.Domain.Entities;
using EVChargingStation.Domain.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EVChargingStation.API.Controllers;

[ApiController]
[Route("api/system")]
public class SystemController : ControllerBase
{
    private readonly EvChargingStationDbContext _context;
    private readonly ILoggerService _logger;


    public SystemController(EvChargingStationDbContext context, ILoggerService logger)
    {
        _context = context;
        _logger = logger;
    }

    [HttpPost("seed-all-data")]
    public async Task<IActionResult> SeedData()
    {
        try
        {
            await ClearDatabase(_context);

            await SeedRolesAndUsers();
            await SeedVehiclesForDefaultUser();
            return Ok(ApiResult<object>.Success(new
            {
                Message = "Data seeded successfully."
            }));
        }
        catch (Exception ex)
        {
            _logger.Error($"Error seeding data: {ex.Message}");
            var statusCode = ExceptionUtils.ExtractStatusCode(ex);
            var errorResponse = ExceptionUtils.CreateErrorResponse<object>(ex);
            return StatusCode(statusCode, errorResponse);
        }
    }


    private async Task ClearDatabase(EvChargingStationDbContext context)
    {
        var strategy = context.Database.CreateExecutionStrategy();

        await strategy.ExecuteAsync(async () =>
        {
            await using var transaction = await context.Database.BeginTransactionAsync();

            try
            {
                _logger.Info("Bắt đầu xóa dữ liệu trong database...");

                var tablesToDelete = new List<Func<Task>>
                {
                    async () =>
                    {
                        _logger.Info("Đang xóa bảng Recommendation...");
                        await context.Recommendations.ExecuteDeleteAsync();
                    },
                    async () =>
                    {
                        _logger.Info("Đang xóa bảng StaffStation...");
                        await context.StaffStations.ExecuteDeleteAsync();
                    },

                    async () =>
                    {
                        _logger.Info("Đang xóa bảng Payment...");
                        await context.Payments.ExecuteDeleteAsync();
                    },
                    async () =>
                    {
                        _logger.Info("Đang xóa bảng Session...");
                        await context.Sessions.ExecuteDeleteAsync();
                    },
                    async () =>
                    {
                        _logger.Info("Đang xóa bảng Reservation...");
                        await context.Reservations.ExecuteDeleteAsync();
                    },
                    async () =>
                    {
                        _logger.Info("Đang xóa bảng Report...");
                        await context.Reports.ExecuteDeleteAsync();
                    },

                    async () =>
                    {
                        _logger.Info("Đang xóa bảng Connector...");
                        await context.Connectors.ExecuteDeleteAsync();
                    },
                    // 2. Xóa các bảng trung gian tiếp theo
                    async () =>
                    {
                        _logger.Info("Đang xóa bảng Vehicle...");
                        await context.Vehicles.ExecuteDeleteAsync();
                    },
                    async () =>
                    {
                        _logger.Info("Đang xóa bảng Station...");
                        await context.Stations.ExecuteDeleteAsync();
                    },
                    // 3. Cuối cùng xóa các bảng cơ bản
                    async () =>
                    {
                        _logger.Info("Đang xóa bảng Location...");
                        await context.Locations.ExecuteDeleteAsync();
                    },
                    async () =>
                    {
                        _logger.Info("Đang xóa bảng User...");
                        await context.Users.ExecuteDeleteAsync();
                    }
                };

                foreach (var deleteFunc in tablesToDelete)
                    await deleteFunc();

                await transaction.CommitAsync();

                _logger.Success("Xóa sạch dữ liệu trong database thành công.");
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                _logger.Error($"Xóa dữ liệu thất bại: {ex.Message}");
                throw;
            }
        });
    }

    private async Task SeedRolesAndUsers()
    {
        _logger.Info("Bắt đầu seed roles và users...");

        var users = new List<User>
        {
            // Admin user
            new()
            {
                FirstName = "Admin",
                LastName = "User",
                Email = "admin@evcharging.com",
                PasswordHash = new PasswordHasher().HashPassword("1@")!,
                Phone = "0123456789",
                Status = UserStatus.Active,
                Role = RoleType.Admin,
                DateOfBirth = new DateTime(1990, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                Gender = Gender.Male,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },

            // Staff user
            new()
            {
                FirstName = "Staff",
                LastName = "User",
                Email = "staff@evcharging.com",
                PasswordHash = new PasswordHasher().HashPassword("1@")!,

                Phone = "0987654321",
                Status = UserStatus.Active,
                Role = RoleType.Staff,
                DateOfBirth = new DateTime(1995, 5, 15, 0, 0, 0, DateTimeKind.Utc),
                Gender = Gender.Male,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },

            // EV Driver user
            new()
            {
                FirstName = "Driver",
                LastName = "User",
                Email = "driver@evcharging.com",
                PasswordHash = new PasswordHasher().HashPassword("1@")!,
                Phone = "0912345678",
                Status = UserStatus.Active,
                Role = RoleType.Driver,
                DateOfBirth = new DateTime(1998, 8, 20, 0, 0, 0, DateTimeKind.Utc),
                Gender = Gender.Female,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            }
        };

        await _context.Users.AddRangeAsync(users);
        await _context.SaveChangesAsync();

        _logger.Success("Seed users thành công");
    }

    private async Task SeedVehiclesForDefaultUser()
    {
        _logger.Info("Bắt đầu seed vehicles cho user mặc định...");

        var driver = await _context.Users.FirstOrDefaultAsync(u => u.Role == RoleType.Driver);
        if (driver == null)
        {
            _logger.Error("Không tìm thấy user mặc định có vai trò Driver.");
            return;
        }

        var vehicles = new List<Vehicle>
        {
            new()
            {
                Make = "Tesla",
                Model = "Model 3",
                Year = 2022,
                LicensePlate = "EV1234",
                ConnectorType = ConnectorType.CCS,
                UserId = driver.Id,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            new()
            {
                Make = "Nissan",
                Model = "Leaf",
                Year = 2021,
                LicensePlate = "EV5678",
                ConnectorType = ConnectorType.CHAdeMO,
                UserId = driver.Id,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            new()
            {
                Make = "Hyundai",
                Model = "Kona Electric",
                Year = 2023,
                LicensePlate = "EV9012",
                ConnectorType = ConnectorType.CCS,
                UserId = driver.Id,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            }
        };

        await _context.Vehicles.AddRangeAsync(vehicles);
        await _context.SaveChangesAsync();

        _logger.Success("Seed vehicles cho user mặc định thành công.");
    }
}