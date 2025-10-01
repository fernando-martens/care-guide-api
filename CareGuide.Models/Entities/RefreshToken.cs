using CareGuide.Models.Entities.Shared;
using CareGuide.Models.Tables;

namespace CareGuide.Models.Entities
{
    public class RefreshToken : Entity
    {
        public Guid UserId { get; set; }
        public string Token { get; set; } = string.Empty;
        public DateTime ExpiresAt { get; set; }
        public bool Revoked { get; set; }
        public User User { get; set; }
    }
}
