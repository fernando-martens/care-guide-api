using CareGuide.Models.Enums;
using System.Text.Json.Serialization;

namespace CareGuide.Models.DTOs.PersonHealth
{
    public record PersonHealthDto(
        Guid Id,
        Guid PersonId,
        [property: JsonConverter(typeof(JsonStringEnumConverter))] BloodType BloodType,
        decimal Height,
        decimal Weight,
        string? Description,
        DateTime CreatedAt,
        DateTime UpdatedAt
    );
}
