using CareGuide.Data.Interfaces;
using CareGuide.Models.Tables;

namespace CareGuide.Data.Repositories
{
    public class UserRepository(DatabaseContext context) : BaseRepository<User>(context), IUserRepository
    {
        public User? GetByEmail(string email)
        {
            return context.Set<User>().FirstOrDefault(u => u.Email == email);
        }
    }
}
