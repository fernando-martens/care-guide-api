using CareGuide.Data.Interfaces;
using CareGuide.Models.Tables;

namespace CareGuide.Data.Repositories
{
    public class PersonRepository : BaseRepository<Person>, IPersonRepository
    {
        private readonly DatabaseContext _context;

        public PersonRepository(DatabaseContext context) : base(context)
        {
            _context = context;
        }
    }
}
