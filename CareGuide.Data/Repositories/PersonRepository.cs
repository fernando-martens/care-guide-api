using CareGuide.Data.Interfaces;
using CareGuide.Models.Tables;

namespace CareGuide.Data.Repositories
{
    public class PersonRepository : BaseRepository, IPersonRepository
    {
        public PersonRepository(DatabaseContext context) : base(context)
        {
        }

        public List<Person> ListAll()
        {
            return _context.Set<Person>().ToList();
        }

        public Person ListById(Guid id)
        {
            return _context.Set<Person>().Find(id)
                   ?? throw new InvalidOperationException($"Person with ID {id} was not found.");
        }

        public Person Insert(Person person)
        {
            _context.Set<Person>().Add(person);
            _context.SaveChanges();
            return person;
        }

        public Person Update(Person person)
        {
            _context.SaveChanges();
            return person;
        }

        public void Remove(Person person)
        {
            _context.Set<Person>().Remove(person);
            _context.SaveChanges();
        }
    }
}
