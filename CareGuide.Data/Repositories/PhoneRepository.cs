using CareGuide.Data.Interfaces;
using CareGuide.Models.Entities;

namespace CareGuide.Data.Repositories
{
    public class PhoneRepository : BaseRepository<Phone>, IPhoneRepository
    {
        private readonly DatabaseContext _context;

        public PhoneRepository(DatabaseContext context) : base(context)
        {
            _context = context;
        }
    }
}
