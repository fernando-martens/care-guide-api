using CareGuide.Models.Enums;
using System.Text.Json.Serialization;

namespace CareGuide.Models.DTOs.PersonHealth
{
    public record CreatePersonHealthDto(
        [property: JsonConverter(typeof(JsonStringEnumConverter))] BloodType BloodType,
        decimal Height,
        decimal Weight,
        string? Description
    );
}
