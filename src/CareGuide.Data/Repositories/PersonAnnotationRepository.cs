using CareGuide.Data.Interfaces;
using CareGuide.Data.Repositories.Shared;
using CareGuide.Models.Entities;
using CareGuide.Security.Interfaces;

namespace CareGuide.Data.Repositories
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
