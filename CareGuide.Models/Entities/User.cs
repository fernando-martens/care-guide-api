using CareGuide.Models.Entities.Shared;

namespace CareGuide.Models.Tables
{
    public class User : Entity
    {
        public Guid PersonId { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
        public Person Person { get; set; }
    }
}
