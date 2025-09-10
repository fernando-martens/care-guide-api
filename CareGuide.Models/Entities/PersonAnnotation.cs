using CareGuide.Models.Entities.Shared;

namespace CareGuide.Models.Tables
{
    public class PersonAnnotation : Entity
    {
        public Guid PersonId { get; set; }
        public string? Details { get; set; }
        public string? FileUrl { get; set; }
        public Person Person { get; set; }
    }
}
