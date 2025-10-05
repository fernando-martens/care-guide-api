using CareGuide.Models.Enums;

namespace CareGuide.Models.DTOs.Phone
{
    public record UpdatePhoneDto(
        Guid Id,
        string Number,
        string AreaCode,
        PhoneType Type
    );
}
