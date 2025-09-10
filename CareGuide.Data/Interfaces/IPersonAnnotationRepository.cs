using CareGuide.Data.Interfaces.Shared;
using CareGuide.Models.Tables;

namespace CareGuide.Data.Interfaces
{
    public interface IPersonAnnotationRepository : IRepository<PersonAnnotation>
    {
        Task<List<PersonAnnotation>> GetAllByPersonAsync(Guid personId, CancellationToken cancellationToken);
        Task RemoveAllByPersonAsync(Guid personId, CancellationToken cancellationToken);
    }
}
