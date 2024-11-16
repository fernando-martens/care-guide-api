using CareGuide.Models.DTOs.Person;
using CareGuide.Models.DTOs.User;
using CareGuide.Models.Enums;

namespace CareGuide.Models.DTOs.Auth
{
    public class AccountDto
    {
        public AccountDto(UserDto user, PersonDto person, string token)
        {
            Id = user.Id;
            Email = user.Email;
            SessionToken = token;
            Name = person.Name;
            Gender = person.Gender;
            Birthday = person.Birthday;
            Picture = person.Picture;
        }

        public Guid Id { get; set; }
        public string Email { get; set; }
        public string SessionToken { get; set; }
        public string Name { get; set; }
        public Gender Gender { get; set; }
        public DateOnly Birthday { get; set; }
        public string Picture { get; set; }

    }
}
