namespace CareGuide.Models.DTOs.User
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public Guid PersonId { get; set; }
        public required string Email { get; set; }
    }
}
