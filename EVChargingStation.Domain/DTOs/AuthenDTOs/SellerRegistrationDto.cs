using System.ComponentModel;
using System.Text.Json.Serialization;

namespace BlindTreasure.Domain.DTOs.AuthenDTOs;

public class SellerRegistrationDto
{
    [DefaultValue("trangiaphuc362003181@gmail.com")]
    public required string Email { get; set; }

    [DefaultValue("trangiaphuc362003181")] public required string Password { get; set; }

    [JsonIgnore] public string? FullName { get; set; } = "";

    [JsonIgnore] public string? PhoneNumber { get; set; } = "";

    [JsonIgnore] public DateTime DateOfBirth { get; set; } = DateTime.UtcNow;

    [JsonIgnore] public string? CompanyName { get; set; } = "";

    [JsonIgnore] public string? TaxId { get; set; } = "";

    [JsonIgnore] public string? CompanyAddress { get; set; } = "";
    [JsonIgnore] public string? CompanyProductDescription { get; set; } = "";
}