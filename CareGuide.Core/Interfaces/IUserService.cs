using CareGuide.Models.DTOs.Auth;
using CareGuide.Models.DTOs.Person;
using CareGuide.Models.DTOs.User;
using CareGuide.Models.Entities;

namespace CareGuide.Core.Interfaces
{
    public interface IUserService
    {
        Task<List<UserDto>> GetAllAsync(int page, int pageSize, CancellationToken cancellationToken);
        Task<UserDto> GetByIdDtoAsync(Guid id, CancellationToken cancellationToken);
        Task<User> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<UserDto> CreateAsync(PersonDto person, CreateUserDto createUser, CancellationToken cancellationToken);
        Task UpdatePasswordAsync(Guid id, UpdatePasswordAccountDto user, CancellationToken cancellationToken);
        Task DeleteAsync(Guid id, CancellationToken cancellationToken);
    }
}
