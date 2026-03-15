using CareGuide.Infra.Contexts;
using CareGuide.Infra.Interfaces;
using CareGuide.Infra.Repositories.Shared;
using CareGuide.Models.Entities;
using CareGuide.Security.Interfaces;

namespace CareGuide.Infra.Repositories
{
    public class PersonAnnotationRepository : BasePersonOwnedRepository<PersonAnnotation>, IPersonAnnotationRepository
    {
        private readonly DatabaseContext _context;

        public PersonAnnotationRepository(DatabaseContext context, IUserSessionContext userSessionContext) : base(context, userSessionContext)
        {
            _context = context;
        }
    }
}
