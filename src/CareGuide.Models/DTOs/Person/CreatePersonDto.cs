using CareGuide.Models.Enums;
using System.Text.Json.Serialization;

namespace CareGuide.Models.DTOs.Person
{
    public record CreatePersonDto(
        Guid Id,
        string Name,
        [property: JsonConverter(typeof(JsonStringEnumConverter))] Gender Gender,
        DateOnly Birthday,
        string? Picture
    );
}
