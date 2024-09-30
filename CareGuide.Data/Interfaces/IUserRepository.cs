using CareGuide.Models.Tables;


namespace CareGuide.Data.Interfaces
{
    public interface IUserRepository
    {
        void Insert(User table);
        List<User> ListAll();
    }
}
