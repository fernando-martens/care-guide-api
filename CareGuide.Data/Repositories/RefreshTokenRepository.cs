using CareGuide.Data.Interfaces;
using CareGuide.Models.Entities;
using CareGuide.Security.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CareGuide.Data.Repositories
{
    public class RefreshTokenRepository : BaseRepository<RefreshToken>, IRefreshTokenRepository
    {
        private readonly DatabaseContext _context;

        public RefreshTokenRepository(DatabaseContext context, IUserSessionContext userSessionContext) : base(context)
        {
            _context = context;
        }

        public async Task<RefreshToken?> GetByTokenAsync(Guid userId, string token, CancellationToken cancellationToken)
        {
            return await _context.Set<RefreshToken>()
                .Where(rt => rt.UserId == userId && rt.Token == token && !rt.Revoked)
                .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<IEnumerable<RefreshToken>> GetAllByUserAsync(Guid userId, CancellationToken cancellationToken)
        {
            return await _context.Set<RefreshToken>()
                .Where(rt => rt.UserId == userId && !rt.Revoked)
                .ToListAsync(cancellationToken);
        }

        public async Task InvalidateAndReplaceAsync(RefreshToken oldToken, RefreshToken newToken, CancellationToken cancellationToken)
        {
            _context.Update(oldToken);
            await _context.AddAsync(newToken, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}