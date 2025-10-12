namespace BlindTreasure.Domain.DTOs.AuthenDTOs;

public class ResetPasswordDto
{
    public string Email { get; set; }
    public string NewPassword { get; set; }
    public string Otp { get; set; }
}

public class ForgotPasswordRequestDto
{
    public string Email { get; set; }
}