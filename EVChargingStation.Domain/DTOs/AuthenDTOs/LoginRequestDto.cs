using System.ComponentModel;

namespace BlindTreasure.Domain.DTOs.AuthenDTOs;

public class LoginRequestDto
{
    [DefaultValue("blindtreasurefpt@gmail.com")]
    public string? Email { get; set; }

    [DefaultValue("1@")] public string? Password { get; set; }

    [DefaultValue(false)] public bool? IsLoginGoole { get; set; } = false;
}

public class GoogleLoginRequestDto
{
    /// <summary>
    ///     Google ID Token trả về từ phía client (Google Sign-In).
    /// </summary>
    public string? Token { get; set; }
}