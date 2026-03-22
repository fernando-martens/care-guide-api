using CareGuide.Models.Enums;
using System.Text.Json.Serialization;

namespace CareGuide.Models.DTOs.Account
{
    public record CreateAccountDto(
        string Email,
        string Password,
        string Name,
        [property: JsonConverter(typeof(JsonStringEnumConverter))] Gender Gender,
        DateOnly Birthday
    );
}
