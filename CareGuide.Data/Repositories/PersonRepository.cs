using CareGuide.Data.Interfaces;
using CareGuide.Models.Tables;

namespace CareGuide.Data.Repositories
{
    public class PersonRepository : BaseRepository, IPersonRepository
    {
        public PersonRepository(DatabaseContext context) : base(context)
        {
        }

        public List<PersonTable> ListAll()
        {
            return _context.Set<PersonTable>().ToList();
        }

        public PersonTable ListById(Guid id)
        {
            return _context.Set<PersonTable>().Find(id)
                   ?? throw new InvalidOperationException($"Person with ID {id} was not found.");
        }

        public PersonTable Insert(PersonTable person)
        {
            _context.Set<PersonTable>().Add(person);
            _context.SaveChanges();
            return person;
        }

        public PersonTable Update(PersonTable person)
        {
            _context.SaveChanges();
            return person;
        }

        public void Remove(PersonTable person)
        {
            _context.Set<PersonTable>().Remove(person);
            _context.SaveChanges();
        }
    }
}
