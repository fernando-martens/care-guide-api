namespace CareGuide.Models.DTOs.Account
{
    public record RefreshTokenDto(
        string RefreshToken,
        string Email
    );
}
