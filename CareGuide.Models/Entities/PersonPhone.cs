using CareGuide.Models.Entities.Shared;

namespace CareGuide.Models.Entities
{
    public class PersonPhone : Entity, IPersonOwnedEntity
    {
        public Guid? PersonId { get; set; }
        public Guid PhoneId { get; set; }

        public Person Person { get; set; } = null!;
        public Phone Phone { get; set; } = null!;
    }
}
