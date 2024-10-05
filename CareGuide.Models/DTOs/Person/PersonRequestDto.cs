using CareGuide.Models.Enums;

namespace CareGuide.Models.DTOs.Person
{
    public class PersonRequestDto
    {
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public Gender Gender { get; set; }
        public DateOnly Birthday { get; set; }
    }
}
