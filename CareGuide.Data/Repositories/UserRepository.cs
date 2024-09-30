using CareGuide.Data.Interfaces;
using CareGuide.Models.Tables;

namespace CareGuide.Data.Repositories
{
    public class UserRepository : BaseRepository, IUserRepository
    {
        public UserRepository(DatabaseContext context) : base(context)
        {
        }

        public void Insert(User table)
        {
            _context.Set<User>().Add(table);
            _context.SaveChanges();
        }

        public List<User> ListAll()
        {
            return _context.Set<User>().ToList();
        }

    }
}
