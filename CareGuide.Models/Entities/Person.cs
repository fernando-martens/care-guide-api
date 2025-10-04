using CareGuide.Models.Entities.Shared;
using CareGuide.Models.Enums;

namespace CareGuide.Models.Entities
{
    public class Person : Entity
    {
        public required string Name { get; set; }
        public required Gender Gender { get; set; }
        public required DateOnly Birthday { get; set; }
        public string? Picture { get; set; }
        public PersonHealth PersonHealth { get; set; }
        public ICollection<PersonAnnotation> PersonAnnotations { get; set; } = new List<PersonAnnotation>();
    }
}
