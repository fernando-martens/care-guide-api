using CareGuide.Models.Entities;
using CareGuide.Models.Entities.Shared;

namespace CareGuide.Models.Tables
{
    public class User : Entity
    {
        public required Guid PersonId { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
        public Person Person { get; set; }
        public ICollection<RefreshToken> RefreshTokens { get; set; } = new List<RefreshToken>();
    }
}
