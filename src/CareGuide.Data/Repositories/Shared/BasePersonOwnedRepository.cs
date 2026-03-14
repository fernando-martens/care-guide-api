using CareGuide.Data.Interfaces.Shared;
using CareGuide.Models.Entities.Shared;
using CareGuide.Security.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CareGuide.Data.Repositories.Shared
{
    public class BasePersonOwnedRepository<TEntity> : BaseRepository<TEntity>, IBasePersonOwnedRepository<TEntity> where TEntity : Entity, IPersonOwnedEntity
    {
        private readonly Guid _personId;

        public BasePersonOwnedRepository(DbContext context, IUserSessionContext userSessionContext) : base(context)
        {
            _personId = userSessionContext.PersonId;
        }

        public async Task<List<TEntity>> GetAllByPersonAsync(int page, int pageSize, CancellationToken cancellationToken = default)
        {
            return await context.Set<TEntity>()
                .Where(e => e.PersonId == _personId)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);
        }

        public async Task<TEntity?> GetFirstByPersonAsync(CancellationToken cancellationToken = default)
        {
            return await context.Set<TEntity>()
                .Where(e => e.PersonId == _personId)
                .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<int> DeleteAllByPersonAsync(CancellationToken cancellationToken = default)
        {
            var set = context.Set<TEntity>();
            var entities = await set.Where(e => e.PersonId == _personId).ToListAsync(cancellationToken);

            if (entities.Count == 0)
                return 0;

            set.RemoveRange(entities);
            return await context.SaveChangesAsync(cancellationToken);
        }
    }
}
