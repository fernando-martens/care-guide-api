using CareGuide.Data.Interfaces;
using CareGuide.Data.Repositories.Shared;
using CareGuide.Models.Entities;
using CareGuide.Security.Interfaces;

namespace CareGuide.Data.Repositories
{
    public class PersonHealthRepository : BasePersonOwnedRepository<PersonHealth>, IPersonHealthRepository
    {
        private readonly DatabaseContext _context;

        public PersonHealthRepository(DatabaseContext context, IUserSessionContext userSessionContext) : base(context, userSessionContext)
        {
            _context = context;
        }
    }
}
