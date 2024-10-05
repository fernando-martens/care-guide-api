using CareGuide.Data.Interfaces;
using CareGuide.Models.Tables;

namespace CareGuide.Data.Repositories
{
    public class UserRepository : BaseRepository, IUserRepository
    {
        public UserRepository(DatabaseContext context) : base(context)
        {
        }

        public List<User> ListAll()
        {
            return _context.Set<User>().ToList();
        }

        public User ListById(Guid id)
        {
            return _context.Set<User>().Find(id)
                   ?? throw new InvalidOperationException($"User with ID {id} was not found.");
        }

        public User ListByEmail(string email)
        {
            return _context.Set<User>().FirstOrDefault(u => u.Email == email)
                   ?? throw new InvalidOperationException($"User with E-mail {email} was not found.");
        }

        public User Insert(User user)
        {
            _context.Set<User>().Add(user);
            _context.SaveChanges();
            return user;
        }

        public User Update(User user)
        {
            _context.SaveChanges();
            return user;
        }

        public void Remove(User user)
        {
            _context.Set<User>().Remove(user);
            _context.SaveChanges();
        }
    }
}
