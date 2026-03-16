using CareGuide.Infra.Contexts;
using CareGuide.Infra.Interfaces;
using CareGuide.Infra.Repositories.Shared;
using CareGuide.Models.Entities;
using CareGuide.Security.Interfaces;

namespace CareGuide.Infra.Repositories
{
    public class PersonDiseaseRepository : BasePersonOwnedRepository<PersonDisease>, IPersonDiseaseRepository
    {
        private readonly DatabaseContext _context;

        public PersonDiseaseRepository(DatabaseContext context, IUserSessionContext userSessionContext) : base(context, userSessionContext)
        {
            _context = context;
        }
    }
}