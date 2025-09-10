using CareGuide.Models.DTOs.Auth;
using CareGuide.Models.DTOs.Person;
using CareGuide.Models.DTOs.User;
using CareGuide.Models.Tables;

namespace CareGuide.Core.Interfaces
{
    public interface IUserService
    {
        Task<List<UserDto>> GetAllAsync(int page, int pageSize);
        Task<UserDto> GetByIdDtoAsync(Guid id);
        Task<User> GetByIdAsync(Guid id);
        Task<UserDto> CreateAsync(PersonDto person, CreateUserDto createUser);
        Task UpdatePasswordAsync(Guid id, UpdatePasswordAccountDto user);
        Task DeleteAsync(Guid id);
    }
}
