using CareGuide.Data.Interfaces;
using CareGuide.Models.Tables;

namespace CareGuide.Data.Repositories
{
    public class PersonAnnotationRepository(DatabaseContext context) : BaseRepository<PersonAnnotation>(context), IPersonAnnotationRepository
    {
        public List<PersonAnnotation> GetAllByPerson(Guid personId)
        {
            return context.Set<PersonAnnotation>()
                .Where(p => p.PersonId == personId)
                .ToList();
        }

        public void RemoveAllByPerson(Guid personId)
        {
            throw new NotImplementedException();
        }
    }
}
