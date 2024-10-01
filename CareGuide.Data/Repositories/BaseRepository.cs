namespace CareGuide.Data.Repositories
{
    public class BaseRepository
    {
        protected readonly DatabaseContext _context;
        public BaseRepository(DatabaseContext context)
        {
            _context = context;
        }

    }
}
