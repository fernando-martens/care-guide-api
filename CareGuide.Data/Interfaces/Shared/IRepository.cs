using CareGuide.Models.Entities.Shared;

namespace CareGuide.Data.Interfaces.Shared
{
    public interface IRepository<T> where T : Entity
    {
        List<T> GetAll(int page, int pageSize);
        T Get(Guid id);
        T Add(T entity);
        T Update(T entity);
        T Delete(Guid id);
        List<T> DeleteMany(IEnumerable<Guid> ids);
        List<T> GetMany(IEnumerable<Guid> ids, int page, int pageSize);
        bool Exists(Guid id);
        int CountExisting(IEnumerable<Guid> ids);
    }
}
