using CareGuide.Models.DTOs.Auth;
using CareGuide.Models.DTOs.Person;
using CareGuide.Models.DTOs.User;
using CareGuide.Models.Tables;

namespace CareGuide.Core.Interfaces
{
    public interface IUserService
    {
        List<UserDto> ListAll();
        UserDto SelectByIdAsDto(Guid id);
        UserTable SelectById(Guid id);
        UserDto Create(PersonDto person, CreateUserDto user);
        void UpdatePassword(Guid id, UpdatePasswordAccountDto user);
        void Delete(Guid id);
    }
}
