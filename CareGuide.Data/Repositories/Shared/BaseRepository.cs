using CareGuide.Data.Interfaces.Shared;
using CareGuide.Models.Entities.Shared;
using Microsoft.EntityFrameworkCore;

namespace CareGuide.Data.Repositories
{
    public class BaseRepository<TEntity>(DbContext context) : IRepository<TEntity> where TEntity : Entity
    {
        public TEntity Add(TEntity entity)
        {
            context.Set<TEntity>().Add(entity);
            context.SaveChanges();
            return entity;
        }

        public TEntity Delete(Guid id)
        {
            var entity = context.Set<TEntity>().Find(id);

            if (entity == null)
                return null!;

            context.Remove(entity);
            context.SaveChanges();
            return entity;
        }

        public List<TEntity> DeleteMany(IEnumerable<Guid> ids)
        {
            var set = context.Set<TEntity>();

            var entities = set
                .Where(e => ids.Contains(e.Id))
                .ToList();

            if (entities.Count == 0)
                return new List<TEntity>();

            set.RemoveRange(entities);
            context.SaveChanges();
            return entities;
        }

        public bool Exists(Guid id)
        {
            return context.Set<TEntity>().Any(e => e.Id == id);
        }

        public int CountExisting(IEnumerable<Guid> ids)
        {
            var idList = ids?.Distinct().ToList() ?? [];
            if (idList.Count == 0) return 0;

            return context.Set<TEntity>()
                .AsNoTracking()
                .Count(e => idList.Contains(e.Id));
        }

        public virtual List<TEntity> GetAll(int page, int pageSize)
        {
            if (page < 1) page = 1;
            if (pageSize < 1) pageSize = 10;

            return context.Set<TEntity>()
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();
        }

        public virtual TEntity Get(Guid id)
        {
            return context.Set<TEntity>().Find(id) ?? null!;
        }

        public virtual List<TEntity> GetMany(IEnumerable<Guid> ids, int page, int pageSize)
        {
            if (page < 1) page = 1;
            if (pageSize < 1) pageSize = 10;

            return context.Set<TEntity>()
                .Where(e => ids.Contains(e.Id))
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();
        }

        public TEntity Update(TEntity entity)
        {
            context.Entry(entity).State = EntityState.Modified;
            context.Set<TEntity>().Update(entity);
            context.SaveChanges();
            return entity;
        }
    }
}
