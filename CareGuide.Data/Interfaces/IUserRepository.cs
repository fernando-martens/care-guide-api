using CareGuide.Models.Tables;


namespace CareGuide.Data.Interfaces
{
    public interface IUserRepository
    {
        List<User> ListAll();
        User ListById(Guid id);
        User ListByEmail(string email);
        User Insert(User user);
        void UpdatePassword(User user);
        void Remove(Guid id);
        User UpdateSessionToken(User user, string token);
    }
}
