using EVChargingStation.Domain.Enums;

namespace EVChargingStation.Domain.DTOs.VehicleDTOs;

public class VehicleDto
{
    public Guid Id { get; set; }
    public string Make { get; set; } = string.Empty;
    public string Model { get; set; } = string.Empty;
    public int? Year { get; set; }
    public string? LicensePlate { get; set; }
    public ConnectorType ConnectorType { get; set; }
}