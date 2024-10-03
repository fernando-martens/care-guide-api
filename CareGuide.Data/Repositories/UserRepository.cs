using CareGuide.Data.Interfaces;
using CareGuide.Models.Tables;
using CareGuide.Security;

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

        public void UpdatePassword(User user)
        {
            User existingUser = ListById(user.Id);

            if (PasswordManager.ValidatePassword(user.Password, existingUser.Password))
            {
                throw new InvalidOperationException("The new password cannot be the same as the current password.");
            }

            existingUser.Password = PasswordManager.HashPassword(user.Password);
            existingUser.Register = user.Register;

            _context.SaveChanges();
        }

        public void Remove(Guid id)
        {
            User existingUser = ListById(id);

            _context.Set<User>().Remove(existingUser);
            _context.SaveChanges();
        }

        public User UpdateSessionToken(User user, string token)
        {
            User existingUser = ListById(user.Id);

            existingUser.SessionToken = token;
            existingUser.Register = user.Register;

            _context.SaveChanges();

            return existingUser;
        }
    }
}
