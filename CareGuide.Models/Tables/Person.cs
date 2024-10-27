using CareGuide.Models.DTOs.Person;
using CareGuide.Models.Enums;

namespace CareGuide.Models.Tables
{
    public class Person
    {
        public Person(PersonRequestDto person)
        {
            Id = Guid.NewGuid();
            UserId = person.UserId;
            Name = person.Name;
            Gender = person.Gender;
            Birthday = person.Birthday;
            Register = DateTime.UtcNow;
            Picture = person.Picture;
        }

        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public Gender Gender { get; set; }
        public DateOnly Birthday { get; set; }
        public DateTime Register { get; set; }
        public string Picture { get; set; }
        public User User { get; set; }
    }
}
