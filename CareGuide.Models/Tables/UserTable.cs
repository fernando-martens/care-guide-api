using CareGuide.Models.DTOs.Person;
using CareGuide.Models.DTOs.User;
using CareGuide.Security;

namespace CareGuide.Models.Tables
{
    public class UserTable
    {
        public UserTable() { }
        public UserTable(PersonDto person, CreateUserDto createUser)
        {
            Id = Guid.NewGuid();
            Register = DateTime.UtcNow;
            Email = createUser.Email;
            Password = PasswordManager.HashPassword(createUser.Password);
            PersonId = person.Id;
        }

        public Guid Id { get; set; }
        public Guid PersonId { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime Register { get; set; }
        public PersonTable Person { get; set; }
    }
}
