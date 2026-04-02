using CareGuide.Infra.Contexts;
using CareGuide.Infra.Interfaces;
using CareGuide.Infra.Repositories.Shared;
using CareGuide.Models.Entities;
using CareGuide.Security.Interfaces;

namespace CareGuide.Infra.Repositories
{
    public class PersonFamilyHistoryRepository : BasePersonOwnedRepository<PersonFamilyHistory>, IPersonFamilyHistoryRepository
    {
        private readonly DatabaseContext _context;

        public PersonFamilyHistoryRepository(DatabaseContext context, IUserSessionContext userSessionContext) : base(context, userSessionContext)
        {
            _context = context;
        }
    }
}