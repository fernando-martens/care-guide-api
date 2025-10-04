using CareGuide.Models.Entities.Shared;
using CareGuide.Models.Enums;

namespace CareGuide.Models.Entities
{
    public class PersonHealth : Entity, IPersonOwnedEntity
    {
        public required Guid? PersonId { get; set; }
        public required BloodType BloodType { get; set; }
        public required decimal Height { get; set; }
        public required decimal Weight { get; set; }
        public string? Description { get; set; }
        public Person Person { get; set; }
    }
}
