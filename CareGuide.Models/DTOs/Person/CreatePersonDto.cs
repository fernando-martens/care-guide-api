using CareGuide.Models.Enums;

namespace CareGuide.Models.DTOs.Person
{
    public record CreatePersonDto(
        Guid Id,
        string Name,
        Gender Gender,
        DateOnly Birthday,
        string? Picture
    );
}
