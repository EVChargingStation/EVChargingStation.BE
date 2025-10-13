using EVChargingStation.Domain.DTOs.VehicleDTOs;

namespace EVChargingStation.Application.Interfaces;

public interface IVehicleService
{
    // Vehicle Management
    Task<List<VehicleDto>> GetUserVehiclesAsync(Guid userId);
    Task<VehicleDto> AddVehicleAsync(Guid userId, VehicleDto vehicleDto);
    Task<VehicleDto> UpdateVehicleAsync(Guid userId, Guid vehicleId, VehicleDto vehicleDto);
    Task<bool> DeleteVehicleAsync(Guid userId, Guid vehicleId);
}