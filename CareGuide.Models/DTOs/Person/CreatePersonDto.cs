using CareGuide.Models.Enums;

namespace CareGuide.Models.DTOs.Person
{
    public class CreatePersonDto
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public Gender Gender { get; set; }
        public DateOnly Birthday { get; set; }
        public string? Picture { get; set; }
    }
}
