using CareGuide.Models.Entities.Shared;

namespace CareGuide.Models.Entities
{
    public class PersonFamilyHistory : Entity, IPersonOwnedEntity
    {
        public required Guid? PersonId { get; set; }
        public required string Relationship { get; set; }
        public required string Diagnosis { get; set; }
        public int? AgeAtDiagnosis { get; set; }

        public Person Person { get; set; } = null!;
    }
}
