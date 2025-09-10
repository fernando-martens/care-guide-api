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

        public async Task<List<PersonAnnotation>> GetAllByPersonAsync(Guid personId)
        {
            return await _context.Set<PersonAnnotation>()
                .Where(p => p.PersonId == personId)
                .ToListAsync();
        }

        public async Task RemoveAllByPersonAsync(Guid personId)
        {
            var annotations = await _context.Set<PersonAnnotation>()
                .Where(p => p.PersonId == personId)
                .ToListAsync();

            _context.Set<PersonAnnotation>().RemoveRange(annotations);
            await _context.SaveChangesAsync();
        }
    }
}
