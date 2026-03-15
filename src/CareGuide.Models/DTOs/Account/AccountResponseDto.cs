using CareGuide.Models.Enums;

namespace CareGuide.Models.DTOs.Account
{
    public record AccountResponseDto(
       Guid Id,
       string Email,
       string Name,
       Gender Gender,
       DateOnly Birthday
   );
}
