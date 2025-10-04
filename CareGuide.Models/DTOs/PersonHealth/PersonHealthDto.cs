using CareGuide.Models.Enums;

namespace CareGuide.Models.DTOs.PersonHealth
{
    public record PersonHealthDto(
        Guid Id,
        Guid PersonId,
        BloodType BloodType,
        decimal Height,
        decimal Weight,
        string? Description,
        DateTime CreatedAt,
        DateTime UpdatedAt
    );
}
