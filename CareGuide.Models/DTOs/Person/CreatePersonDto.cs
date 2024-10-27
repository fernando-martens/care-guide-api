using CareGuide.Models.DTOs.Auth;
using CareGuide.Models.Enums;

namespace CareGuide.Models.DTOs.Person
{
    public class CreatePersonDto
    {
        public CreatePersonDto(CreateAccountDto createAccount) 
        {
            Id = Guid.NewGuid();
            Name = createAccount.Name;
            Gender = createAccount.Gender;
            Birthday = createAccount.Birthday;
            Picture = "";
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public Gender Gender { get; set; }
        public DateOnly Birthday { get; set; }
        public string Picture { get; set; }
    }
}
