using CareGuide.Models.Enums;

namespace CareGuide.Models.DTOs.Phone
{
    public record PhoneDto(
        Guid Id,
        string Number,
        string AreaCode,
        PhoneType Type,
        DateTime CreatedAt,
        DateTime UpdatedAt
    );
}
