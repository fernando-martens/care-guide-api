using CareGuide.Models.Enums;

namespace CareGuide.Models.DTOs.Auth
{
    public class CreateAccountDto
    {
        public required string Email { get; set; }
        public required string Password { get; set; }
        public required string Name { get; set; }
        public Gender Gender { get; set; }
        public DateOnly Birthday { get; set; }
    }
}
