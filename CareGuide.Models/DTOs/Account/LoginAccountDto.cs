namespace CareGuide.Models.DTOs.Auth
{
    public class LoginAccountDto
    {
        public required string Email { get; set; }
        public required string Password { get; set; }
    }
}
