using CareGuide.Data.Interfaces;
using CareGuide.Models.Tables;

namespace CareGuide.Data.Repositories
{
    public class PersonAnnotationRepository : BaseRepository, IPersonAnnotationRepository
    {
        public PersonAnnotationRepository(DatabaseContext context) : base(context)
        {
        }

        public List<PersonAnnotationTable> ListAllByPerson(Guid personId)
        {
            return _context.Set<PersonAnnotationTable>()
                .Where(p => p.PersonId == personId)
                .ToList();
        }

        public PersonAnnotationTable? SelectById(Guid id)
        {
            return _context.Set<PersonAnnotationTable>().Find(id);
        }

        public PersonTable Insert(PersonTable person)
        {
            throw new NotImplementedException();
        }

        public PersonTable Update(PersonTable person)
        {
            throw new NotImplementedException();
        }

        public void RemoveAllByPerson(Guid personId)
        {
            throw new NotImplementedException();
        }

        public void Remove(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
