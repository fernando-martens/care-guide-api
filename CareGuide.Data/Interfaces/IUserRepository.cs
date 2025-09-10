using CareGuide.Data.Interfaces.Shared;
using CareGuide.Models.Tables;


namespace CareGuide.Data.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        User? GetByEmail(string email);
    }
}
