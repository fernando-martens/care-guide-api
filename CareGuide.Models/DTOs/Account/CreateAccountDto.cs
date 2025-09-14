using CareGuide.Models.Enums;

namespace CareGuide.Models.DTOs.Auth
{
    public record CreateAccountDto(
        string Email,
        string Password,
        string Name,
        Gender Gender,
        DateOnly Birthday
    );
}
