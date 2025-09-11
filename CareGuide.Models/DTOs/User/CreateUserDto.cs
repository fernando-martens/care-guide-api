namespace CareGuide.Models.DTOs.User
{
    public class CreateUserDto
    {
        public Guid Id { get; set; }
        public Guid PersonId { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
    }
}
