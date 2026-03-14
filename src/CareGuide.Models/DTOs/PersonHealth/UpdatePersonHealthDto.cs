using CareGuide.Models.Enums;

namespace CareGuide.Models.DTOs.PersonHealth
{
    public record UpdatePersonHealthDto(
        Guid Id,
        BloodType BloodType,
        decimal Height,
        decimal Weight,
        string? Description
    );
}
