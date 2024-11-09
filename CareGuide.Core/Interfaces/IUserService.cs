using CareGuide.Models.DTOs.Person;
using CareGuide.Models.DTOs.User;

namespace CareGuide.Core.Interfaces
{
    public interface IUserService
    {
        List<UserDto> ListAll();
        UserDto Select(Guid id);
        UserDto Create(PersonDto person, CreateUserDto user);
        void UpdatePassword(Guid id, UserUpdatePasswordDto user);
        void Delete(Guid id);
    }
}
