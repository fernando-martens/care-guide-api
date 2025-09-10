using CareGuide.Data.Interfaces.Shared;
using CareGuide.Models.Entities.Shared;
using Microsoft.EntityFrameworkCore;

namespace CareGuide.Data.Repositories
{
    public class BaseRepository<TEntity> : IRepository<TEntity> where TEntity : Entity
    {
        protected readonly DbContext context;

        public BaseRepository(DbContext context)
        {
            this.context = context;
        }

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            await context.Set<TEntity>().AddAsync(entity);
            await context.SaveChangesAsync();
            return entity;
        }

        public async Task<TEntity?> DeleteAsync(Guid id)
        {
            var entity = await context.Set<TEntity>().FindAsync(id);

            if (entity == null)
                return null;

            context.Remove(entity);
            await context.SaveChangesAsync();
            return entity;
        }

        public async Task<List<TEntity>> DeleteManyAsync(IEnumerable<Guid> ids)
        {
            var set = context.Set<TEntity>();
            var entities = await set.Where(e => ids.Contains(e.Id)).ToListAsync();

            if (entities.Count == 0)
                return new List<TEntity>();

            set.RemoveRange(entities);
            await context.SaveChangesAsync();
            return entities;
        }

        public async Task<bool> ExistsAsync(Guid id)
        {
            return await context.Set<TEntity>().AnyAsync(e => e.Id == id);
        }

        public async Task<int> CountExistingAsync(IEnumerable<Guid> ids)
        {
            var idList = ids?.Distinct().ToList() ?? [];
            if (idList.Count == 0) return 0;

            return await context.Set<TEntity>()
                .AsNoTracking()
                .CountAsync(e => idList.Contains(e.Id));
        }

        public virtual async Task<List<TEntity>> GetAllAsync(int page, int pageSize)
        {
            if (page < 1) page = 1;
            if (pageSize < 1) pageSize = 10;

            return await context.Set<TEntity>()
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public virtual async Task<TEntity?> GetAsync(Guid id)
        {
            return await context.Set<TEntity>().FindAsync(id) ?? null;
        }

        public virtual async Task<List<TEntity>> GetManyAsync(IEnumerable<Guid> ids, int page, int pageSize)
        {
            if (page < 1) page = 1;
            if (pageSize < 1) pageSize = 10;

            return await context.Set<TEntity>()
                .Where(e => ids.Contains(e.Id))
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            context.Entry(entity).State = EntityState.Modified;
            context.Set<TEntity>().Update(entity);
            await context.SaveChangesAsync();
            return entity;
        }
    }
}
