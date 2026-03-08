using CareGuide.Models.Enums;

namespace CareGuide.Models.DTOs.Account
{
    public record CreateAccountDto(
        string Email,
        string Password,
        string Name,
        Gender Gender,
        DateOnly Birthday
    );
}
