using CareGuide.Models.Enums;
using System.Text.Json.Serialization;

namespace CareGuide.Models.DTOs.Person
{
    public record PersonDto(
        Guid Id,
        string Name,
        string? Picture,
        [property: JsonConverter(typeof(JsonStringEnumConverter))] Gender Gender,
        DateOnly Birthday,
        DateTime CreatedAt,
        DateTime UpdatedAt
    );
}
