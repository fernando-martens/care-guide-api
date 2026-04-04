using CareGuide.Models.Enums;
using System.Text.Json.Serialization;

namespace CareGuide.Models.DTOs.DoctorPhone
{
    public record DoctorPhoneDto(
        Guid DoctorId,
        Guid PhoneId,
        string Number,
        string AreaCode,
        [property: JsonConverter(typeof(JsonStringEnumConverter))] PhoneType Type
    );
}