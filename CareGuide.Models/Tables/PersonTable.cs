using CareGuide.Models.DTOs.Person;
using CareGuide.Models.Enums;

namespace CareGuide.Models.Tables
{
    public class PersonTable
    {
        public PersonTable() { }
        public PersonTable(CreatePersonDto person)
        {
            Id = Guid.NewGuid();
            Name = person.Name;
            Gender = person.Gender;
            Birthday = person.Birthday;
            Picture = person.Picture;
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public Gender Gender { get; set; }
        public DateOnly Birthday { get; set; }
        public string Picture { get; set; }
        public ICollection<PersonAnnotationTable> PersonAnnotations { get; set; }
    }
}
