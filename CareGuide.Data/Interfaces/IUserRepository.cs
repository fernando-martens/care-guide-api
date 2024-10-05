using CareGuide.Models.Tables;


namespace CareGuide.Data.Interfaces
{
    public interface IUserRepository
    {
        List<User> ListAll();
        User ListById(Guid id);
        User ListByEmail(string email);
        User Insert(User user);
        User Update(User user);
        void Remove(User id);
    }
}
