namespace CareGuide.Models.DTOs.User
{
    public record CreateUserDto(
        Guid Id,
        Guid PersonId,
        string Email,
        string Password
    );
}
