using CareGuide.Infra.Contexts;
using CareGuide.Infra.Interfaces;
using CareGuide.Infra.Repositories.Shared;
using CareGuide.Models.Entities;
using CareGuide.Security.Interfaces;

namespace CareGuide.Infra.Repositories
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
