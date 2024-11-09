using CareGuide.Models.DTOs.Auth;

namespace CareGuide.Models.DTOs.User
{
    public class CreateUserDto
    {
        public CreateUserDto(CreateAccountDto createAccount) 
        {
            Id = Guid.NewGuid();
            Email = createAccount.Email;
            Password = createAccount.Password;
        }

        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
