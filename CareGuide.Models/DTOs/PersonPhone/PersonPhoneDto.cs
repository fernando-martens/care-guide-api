using CareGuide.Models.DTOs.Phone;

namespace CareGuide.Models.DTOs.PersonPhone
{
    public record PersonPhoneDto(
        Guid PersonId,
        ICollection<PhoneDto> Phones
    );
}
