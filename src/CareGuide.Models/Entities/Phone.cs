using CareGuide.Models.Entities.Shared;
using CareGuide.Models.Enums;

namespace CareGuide.Models.Entities
{
    public class Phone : Entity
    {
        public required string Number { get; set; }
        public required string AreaCode { get; set; }
        public required PhoneType Type { get; set; } = PhoneType.O;

        public ICollection<PersonPhone> PersonPhones { get; set; } = new List<PersonPhone>();
    }
}
