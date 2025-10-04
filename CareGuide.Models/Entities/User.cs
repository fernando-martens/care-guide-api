using CareGuide.Models.Entities.Shared;

namespace CareGuide.Models.Entities
{
    public class User : Entity, IPersonOwnedEntity
    {
        public required Guid? PersonId { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
        public Person Person { get; set; }
        public ICollection<RefreshToken> RefreshTokens { get; set; } = new List<RefreshToken>();
    }
}
