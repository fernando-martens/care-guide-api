using CareGuide.Models.Enums;
using System.Text.Json.Serialization;

namespace CareGuide.Models.DTOs.PersonPhone
{
    public record PersonPhoneDto(
        Guid PersonId,
        Guid PhoneId,
        string Number,
        string AreaCode,
        [property: JsonConverter(typeof(JsonStringEnumConverter))] PhoneType Type
    );
}