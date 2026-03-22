using CareGuide.Models.Enums;
using System.Text.Json.Serialization;

namespace CareGuide.Models.DTOs.Account
{
    public record AccountResponseDto(
       Guid Id,
       string Email,
       string Name,
       [property: JsonConverter(typeof(JsonStringEnumConverter))] Gender Gender,
       DateOnly Birthday
   );
}
