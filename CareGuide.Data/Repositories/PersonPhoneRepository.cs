using CareGuide.Data.Interfaces;
using CareGuide.Data.Repositories.Shared;
using CareGuide.Models.Entities;
using CareGuide.Security.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CareGuide.Data.Repositories
{
    public class PersonPhoneRepository : BasePersonOwnedRepository<PersonPhone>, IPersonPhoneRepository
    {
        private readonly DatabaseContext _context;
        private readonly IUserSessionContext _userSessionContext;

        public PersonPhoneRepository(DatabaseContext context, IUserSessionContext userSessionContext) : base(context, userSessionContext)
        {
            _context = context;
            _userSessionContext = userSessionContext;
        }

        public async Task<List<PersonPhone>> GetAllByPersonWithPhonesAsync(int page, int pageSize, CancellationToken cancellationToken = default)
        {
            var personId = _userSessionContext.PersonId;

            return await _context.PersonPhones
                .Include(pp => pp.Phone)
                .Where(pp => pp.PersonId == personId)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);
        }

        public async Task<PersonPhone?> GetByPersonWithPhoneAsync(Guid phoneId, CancellationToken cancellationToken = default)
        {
            var personId = _userSessionContext.PersonId;

            return await _context.PersonPhones
                .Include(pp => pp.Phone)
                .FirstOrDefaultAsync(pp => pp.PersonId == personId && pp.PhoneId == phoneId, cancellationToken);
        }

        public async Task<List<PersonPhone>> GetManyByPersonAndIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken = default)
        {
            var personId = _userSessionContext.PersonId;

            return await _context.PersonPhones
                .Include(pp => pp.Phone)
                .Where(pp => pp.PersonId == personId && ids.Contains(pp.Id))
                .ToListAsync(cancellationToken);
        }
    }
}
