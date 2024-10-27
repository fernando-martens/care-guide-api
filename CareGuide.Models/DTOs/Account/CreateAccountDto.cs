using CareGuide.Models.Enums;

namespace CareGuide.Models.DTOs.Auth
{
    public class CreateAccountDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public Gender Gender { get; set; }
        public DateOnly Birthday { get; set; }
    }
}
