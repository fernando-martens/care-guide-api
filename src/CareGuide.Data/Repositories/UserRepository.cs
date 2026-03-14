using CareGuide.Data.Interfaces;
using CareGuide.Models.Entities;

namespace CareGuide.Data.Repositories
{
    using CareGuide.Data.Repositories.Shared;
    using CareGuide.Security.Interfaces;
    using Microsoft.EntityFrameworkCore;

    public class UserRepository : BasePersonOwnedRepository<User>, IUserRepository
    {
        private readonly DatabaseContext _context;

        public UserRepository(DatabaseContext context, IUserSessionContext userSessionContext) : base(context, userSessionContext)
        {
            _context = context;
        }

        public async Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
        {
            return await _context.Set<User>()
                .IgnoreQueryFilters()
                .FirstOrDefaultAsync(u => u.Email == email, cancellationToken);
        }
    }
}
