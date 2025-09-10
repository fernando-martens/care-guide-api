using CareGuide.Data.Interfaces.Shared;
using CareGuide.Models.Tables;

namespace CareGuide.Data.Interfaces
{
    public interface IPersonAnnotationRepository : IRepository<PersonAnnotation>
    {
        List<PersonAnnotation> ListAllByPerson(Guid personId);
        void RemoveAllByPerson(Guid personId);
    }
}
