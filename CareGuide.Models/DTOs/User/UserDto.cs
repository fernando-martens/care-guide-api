namespace CareGuide.Models.DTOs.User
{
    public record UserDto(
        Guid Id,
        Guid PersonId,
        string Email
    );
}
