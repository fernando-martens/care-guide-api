using CareGuide.Infra.Interfaces.Shared;
using CareGuide.Models.Entities;

namespace CareGuide.Infra.Interfaces
{
    public interface IRefreshTokenRepository : IRepository<RefreshToken>
    {
        Task<RefreshToken?> GetByTokenAsync(Guid userId, string token, CancellationToken cancellationToken);
        Task<IEnumerable<RefreshToken>> GetAllByUserAsync(Guid userId, CancellationToken cancellationToken);
        Task InvalidateAndReplaceAsync(RefreshToken oldToken, RefreshToken newToken, CancellationToken cancellationToken);
    }
}
