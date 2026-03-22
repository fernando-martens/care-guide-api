using CareGuide.Models.Enums;
using System.Text.Json.Serialization;

namespace CareGuide.Models.DTOs.Account
{
    public record AccountDto(
        Guid Id,
        string Email,
        string SessionToken,
        string RefreshToken,
        string Name,
        [property: JsonConverter(typeof(JsonStringEnumConverter))] Gender Gender,
        DateOnly Birthday
    );
}
