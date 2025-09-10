using CareGuide.Data.Interfaces.Shared;
using CareGuide.Models.Tables;

namespace CareGuide.Data.Interfaces
{
    public interface IPersonAnnotationRepository : IRepository<PersonAnnotation>
    {
        List<PersonAnnotation> GetAllByPerson(Guid personId);
        void RemoveAllByPerson(Guid personId);
    }
}
