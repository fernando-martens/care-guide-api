using CareGuide.Data.Interfaces.Shared;
using CareGuide.Models.Entities;


namespace CareGuide.Data.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken);
    }
}
