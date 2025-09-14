using CareGuide.Models.Enums;

namespace CareGuide.Models.DTOs.Person
{
    public record PersonDto(
        Guid Id,
        string Name,
        string? Picture,
        Gender Gender,
        DateOnly Birthday
    );
}
