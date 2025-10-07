using CareGuide.Data.Interfaces.Shared;
using CareGuide.Models.Entities;

namespace CareGuide.Data.Interfaces
{
    public interface IPersonPhoneRepository : IBasePersonOwnedRepository<PersonPhone>
    {
        Task<List<PersonPhone>> GetAllByPersonWithPhonesAsync(int page, int pageSize, CancellationToken cancellationToken);
        Task<PersonPhone?> GetByPersonWithPhoneAsync(Guid phoneId, CancellationToken cancellationToken);
        Task<List<PersonPhone>> GetManyByPersonAndIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken);
    }
}
