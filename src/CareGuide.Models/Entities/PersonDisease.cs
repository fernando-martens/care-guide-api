using CareGuide.Models.Entities.Shared;
using CareGuide.Models.Enums;

namespace CareGuide.Models.Entities
{
    public class PersonDisease : Entity, IPersonOwnedEntity
    {
        public required Guid? PersonId { get; set; }
        public required string Name { get; set; }
        public DateOnly? DiagnosisDate { get; set; }
        public required DiseaseType DiseaseType { get; set; }

        public Person Person { get; set; } = null!;
    }
}
