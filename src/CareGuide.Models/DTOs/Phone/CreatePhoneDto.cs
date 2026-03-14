using CareGuide.Models.Enums;

namespace CareGuide.Models.DTOs.Phone
{
    public record CreatePhoneDto(
        string Number,
        string AreaCode,
        PhoneType Type
    );
}
