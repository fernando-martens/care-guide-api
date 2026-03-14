using CareGuide.Data.Interfaces.Shared;
using CareGuide.Models.Entities;

namespace CareGuide.Data.Interfaces
{
    public interface IRefreshTokenRepository : IRepository<RefreshToken>
    {
        Task<RefreshToken?> GetByTokenAsync(Guid userId, string token, CancellationToken cancellationToken);
        Task<IEnumerable<RefreshToken>> GetAllByUserAsync(Guid userId, CancellationToken cancellationToken);
        Task InvalidateAndReplaceAsync(RefreshToken oldToken, RefreshToken newToken, CancellationToken cancellationToken);
    }
}
