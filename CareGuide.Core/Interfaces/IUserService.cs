using CareGuide.Models.DTOs.User;
using CareGuide.Models.Tables;

namespace CareGuide.Core.Interfaces
{
    public interface IUserService
    {
        List<User> ListAll();
        User ListById(Guid id);
        User Insert(UserRequestDto user);
        void UpdatePassword(Guid id, UserUpdatePasswordDto user);
        void Remove(Guid id);
        User Login(UserRequestDto user);
    }
}
