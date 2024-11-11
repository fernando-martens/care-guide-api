using CareGuide.Data.Interfaces;
using CareGuide.Models.Tables;

namespace CareGuide.Data.Repositories
{
    public class UserRepository : BaseRepository, IUserRepository
    {
        public UserRepository(DatabaseContext context) : base(context)
        {
        }

        public List<UserTable> ListAll()
        {
            return _context.Set<UserTable>().ToList();
        }

        public UserTable? SelectById(Guid id)
        {
            return _context.Set<UserTable>().Find(id);
        }

        public UserTable? SelectByEmail(string email)
        {
            return _context.Set<UserTable>().FirstOrDefault(u => u.Email == email);
        }

        public UserTable Insert(UserTable user)
        {
            _context.Set<UserTable>().Add(user);
            _context.SaveChanges();
            return user;
        }

        public UserTable Update(UserTable user)
        {
            _context.SaveChanges();
            return user;
        }

        public void Remove(UserTable user)
        {
            _context.Set<UserTable>().Remove(user);
            _context.SaveChanges();
        }
    }
}
