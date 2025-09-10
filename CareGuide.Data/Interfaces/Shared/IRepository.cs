using CareGuide.Models.Entities.Shared;

namespace CareGuide.Data.Interfaces.Shared
{
    public interface IRepository<T> where T : Entity
    {
        Task<T> AddAsync(T entity);
        Task<T?> DeleteAsync(Guid id);
        Task<List<T>> DeleteManyAsync(IEnumerable<Guid> ids);
        Task<bool> ExistsAsync(Guid id);
        Task<int> CountExistingAsync(IEnumerable<Guid> ids);
        Task<List<T>> GetAllAsync(int page, int pageSize);
        Task<T?> GetAsync(Guid id);
        Task<List<T>> GetManyAsync(IEnumerable<Guid> ids, int page, int pageSize);
        Task<T> UpdateAsync(T entity);
    }
}
