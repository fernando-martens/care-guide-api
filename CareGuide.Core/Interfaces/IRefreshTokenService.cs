using CareGuide.Models.Entities;

namespace CareGuide.Core.Interfaces
{
    public interface IRefreshTokenService
    {
        Task<RefreshToken> CreateAsync(Guid userId, CancellationToken cancellationToken);
        Task<RefreshToken?> GetAsync(Guid userId, string token, CancellationToken cancellationToken);
        Task<RefreshToken> RotateAsync(Guid userId, string oldToken, CancellationToken cancellationToken);
        Task InvalidateAsync(Guid userId, string token, CancellationToken cancellationToken);
    }
}
