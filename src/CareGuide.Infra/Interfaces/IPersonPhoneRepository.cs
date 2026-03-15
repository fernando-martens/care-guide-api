using CareGuide.Infra.Interfaces.Shared;
using CareGuide.Models.Entities;

namespace CareGuide.Infra.Interfaces
{
    public interface IPersonPhoneRepository : IBasePersonOwnedRepository<PersonPhone>
    {
        Task<List<PersonPhone>> GetAllByPersonWithPhonesAsync(int page, int pageSize, CancellationToken cancellationToken);
        Task<PersonPhone?> GetByPersonWithPhoneAsync(Guid phoneId, CancellationToken cancellationToken);
        Task<List<PersonPhone>> GetManyByPersonAndIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken);
    }
}
