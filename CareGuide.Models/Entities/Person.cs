using CareGuide.Models.Entities.Shared;
using CareGuide.Models.Enums;

namespace CareGuide.Models.Tables
{
    public class Person : Entity
    {
        public required string Name { get; set; }
        public Gender Gender { get; set; }
        public DateOnly Birthday { get; set; }
        public string? Picture { get; set; }
        public ICollection<PersonAnnotation> PersonAnnotations { get; set; }
    }
}
