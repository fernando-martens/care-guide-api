using CareGuide.Models.Enums;
using System.Text.Json.Serialization;

namespace CareGuide.Models.DTOs.Phone
{
    public record UpdatePhoneDto(
        Guid Id,
        string Number,
        string AreaCode,
        [property: JsonConverter(typeof(JsonStringEnumConverter))] PhoneType Type
    );
}
