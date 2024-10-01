using CareGuide.Models.Tables;

namespace CareGuide.Core.Interfaces
{
    public interface IUserService
    {
        Guid Insert(User user);
        List<User> ListAll();
    }
}
