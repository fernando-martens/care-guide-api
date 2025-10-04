using CareGuide.Data.Interfaces;
using CareGuide.Models.Entities;
using CareGuide.Security.Interfaces;

namespace CareGuide.Data.Repositories
{
    public class PersonRepository : BaseRepository<Person>, IPersonRepository
    {
        private readonly DatabaseContext _context;

        public PersonRepository(DatabaseContext context, IUserSessionContext userSessionContext) : base(context)
        {
            _context = context;
        }
    }
}
