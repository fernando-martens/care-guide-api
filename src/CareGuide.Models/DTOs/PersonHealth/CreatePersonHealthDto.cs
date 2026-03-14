using CareGuide.Models.Enums;

namespace CareGuide.Models.DTOs.PersonHealth
{
    public record CreatePersonHealthDto(
        BloodType BloodType,
        decimal Height,
        decimal Weight,
        string? Description
    );
}
