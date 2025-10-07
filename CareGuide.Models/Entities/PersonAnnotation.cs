using CareGuide.Models.Entities.Shared;

namespace CareGuide.Models.Entities
{
    public class PersonAnnotation : Entity, IPersonOwnedEntity
    {
        public required Guid? PersonId { get; set; }
        public required string Details { get; set; }
        public string? FileUrl { get; set; }
        
        public Person Person { get; set; } = null!;
    }
}
