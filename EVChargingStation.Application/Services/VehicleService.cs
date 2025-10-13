using EVChargingStation.Application.Interfaces;
using EVChargingStation.Application.Interfaces.Commons;
using EVChargingStation.Application.Utils;
using EVChargingStation.Domain.DTOs.VehicleDTOs;
using EVChargingStation.Domain.Entities;
using EVChargingStation.Infrastructure.Interfaces;

namespace EVChargingStation.Application.Services;

public class VehicleService : IVehicleService
{
    private readonly ILoggerService _loggerService;
    private readonly IUnitOfWork _unitOfWork;

    public VehicleService(IUnitOfWork unitOfWork, ILoggerService loggerService)
    {
        _unitOfWork = unitOfWork;
        _loggerService = loggerService;
    }

    /// <summary>
    ///     Adds a new vehicle for a user
    /// </summary>
    public async Task<VehicleDto> AddVehicleAsync(Guid userId, VehicleDto vehicleDto)
    {
        var user = await _unitOfWork.Users.GetByIdAsync(userId);
        if (user == null)
            throw ErrorHelper.NotFound(ErrorMessages.AccountNotFound);

        var vehicle = new Vehicle
        {
            Make = vehicleDto.Make,
            Model = vehicleDto.Model,
            Year = vehicleDto.Year,
            LicensePlate = vehicleDto.LicensePlate,
            ConnectorType = vehicleDto.ConnectorType,
            UserId = userId
        };

        await _unitOfWork.Vehicles.AddAsync(vehicle);
        await _unitOfWork.SaveChangesAsync();

        _loggerService.Info($"Vehicle added for user {userId}");

        return ToVehicleDto(vehicle);
    }

    /// <summary>
    ///     Gets all vehicles for a user
    /// </summary>
    public async Task<List<VehicleDto>> GetUserVehiclesAsync(Guid userId)
    {
        var user = await _unitOfWork.Users.GetByIdAsync(userId);
        if (user == null)
            throw ErrorHelper.NotFound(ErrorMessages.AccountNotFound);

        var vehicles = await _unitOfWork.Vehicles.GetAllAsync(v => v.UserId == userId && !v.IsDeleted);
        return vehicles.Select(ToVehicleDto).ToList();
    }

    /// <summary>
    ///     Updates an existing vehicle
    /// </summary>
    public async Task<VehicleDto> UpdateVehicleAsync(Guid userId, Guid vehicleId, VehicleDto vehicleDto)
    {
        var vehicle =
            await _unitOfWork.Vehicles.FirstOrDefaultAsync(v =>
                v.Id == vehicleId && v.UserId == userId && !v.IsDeleted);

        if (vehicle == null)
            throw ErrorHelper.NotFound("Vehicle not found");

        // Update vehicle fields
        vehicle.Make = vehicleDto.Make;
        vehicle.Model = vehicleDto.Model;
        vehicle.Year = vehicleDto.Year;
        vehicle.LicensePlate = vehicleDto.LicensePlate;
        vehicle.ConnectorType = vehicleDto.ConnectorType;

        await _unitOfWork.Vehicles.Update(vehicle);
        await _unitOfWork.SaveChangesAsync();

        _loggerService.Info($"Vehicle {vehicleId} updated for user {userId}");

        return ToVehicleDto(vehicle);
    }

    /// <summary>
    ///     Deletes a vehicle
    /// </summary>
    public async Task<bool> DeleteVehicleAsync(Guid userId, Guid vehicleId)
    {
        var vehicle =
            await _unitOfWork.Vehicles.FirstOrDefaultAsync(v =>
                v.Id == vehicleId && v.UserId == userId && !v.IsDeleted);

        if (vehicle == null)
            throw ErrorHelper.NotFound("Vehicle not found");

        // Soft delete
        await _unitOfWork.Vehicles.SoftRemove(vehicle);
        await _unitOfWork.SaveChangesAsync();

        _loggerService.Info($"Vehicle {vehicleId} deleted for user {userId}");

        return true;
    }

    private static VehicleDto ToVehicleDto(Vehicle vehicle)
    {
        return new VehicleDto
        {
            Id = vehicle.Id,
            Make = vehicle.Make,
            Model = vehicle.Model,
            Year = vehicle.Year,
            LicensePlate = vehicle.LicensePlate,
            ConnectorType = vehicle.ConnectorType
        };
    }
}