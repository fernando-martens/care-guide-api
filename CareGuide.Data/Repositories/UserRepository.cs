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

        public UserTable ListById(Guid id)
        {
            return _context.Set<UserTable>().Find(id)
                   ?? throw new InvalidOperationException($"User with ID {id} was not found.");
        }

        public UserTable ListByEmail(string email)
        {
            return _context.Set<UserTable>().FirstOrDefault(u => u.Email == email)
                   ?? throw new InvalidOperationException($"User with E-mail {email} was not found.");
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
