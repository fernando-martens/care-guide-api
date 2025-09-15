using CareGuide.Models.Enums;

namespace CareGuide.Models.DTOs.Auth
{
    public record AccountDto(
        Guid Id,
        string Email,
        string SessionToken,
        string Name,
        Gender Gender,
        DateOnly Birthday
    );
}
