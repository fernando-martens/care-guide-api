using CareGuide.Models.Tables;


namespace CareGuide.Data.Interfaces
{
    public interface IUserRepository
    {
        List<UserTable> ListAll();
        UserTable? SelectById(Guid id);
        UserTable? SelectByEmail(string email);
        UserTable Insert(UserTable user);
        UserTable Update(UserTable user);
        void Remove(UserTable id);
    }
}
