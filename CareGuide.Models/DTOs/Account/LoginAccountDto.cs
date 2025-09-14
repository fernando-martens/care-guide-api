namespace CareGuide.Models.DTOs.Auth
{
    public record LoginAccountDto(
        string Email,
        string Password
    );
}
