using CareGuide.Data.Interfaces;
using CareGuide.Models.Tables;
using Microsoft.EntityFrameworkCore;

namespace CareGuide.Data.Repositories
{
    public class PersonAnnotationRepository : BaseRepository<PersonAnnotation>, IPersonAnnotationRepository
    {
        private readonly DatabaseContext _context;

        public PersonAnnotationRepository(DatabaseContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<PersonAnnotation>> GetAllByPersonAsync(Guid personId, int page, int pageSize, CancellationToken cancellationToken = default)
        {
            return await _context.Set<PersonAnnotation>()
                .Where(p => p.PersonId == personId)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);
        }

        public async Task DeleteAllByPersonAsync(Guid personId, CancellationToken cancellationToken = default)
        {
            var annotations = await _context.Set<PersonAnnotation>()
                .Where(p => p.PersonId == personId)
                .ToListAsync(cancellationToken);

            _context.Set<PersonAnnotation>().RemoveRange(annotations);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
