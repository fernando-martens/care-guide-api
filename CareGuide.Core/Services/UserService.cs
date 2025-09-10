using AutoMapper;
using CareGuide.Core.Interfaces;
using CareGuide.Data.Interfaces;
using CareGuide.Models.Constants;
using CareGuide.Models.DTOs.Auth;
using CareGuide.Models.DTOs.Person;
using CareGuide.Models.DTOs.User;
using CareGuide.Models.Tables;
using CareGuide.Security;

namespace CareGuide.Core.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<List<UserDto>> GetAllAsync(int page = PaginationConstants.DefaultPage, int pageSize = PaginationConstants.DefaultPageSize)
        {
            var list = await _userRepository.GetAllAsync(page, pageSize);
            return _mapper.Map<List<UserDto>>(list);
        }

        public async Task<UserDto> GetByIdDtoAsync(Guid id)
        {
            var userTable = await GetByIdAsync(id);
            return _mapper.Map<UserDto>(userTable);
        }

        public async Task<User> GetByIdAsync(Guid id)
        {
            var user = await _userRepository.GetAsync(id);

            if (user == null)
                throw new KeyNotFoundException();

            return user;
        }

        public async Task<UserDto> CreateAsync(PersonDto person, CreateUserDto createUser)
        {
            if (await _userRepository.GetByEmailAsync(createUser.Email) != null)
                throw new InvalidOperationException("Email already registered");

            var user = _mapper.Map<User>(createUser);
            await _userRepository.AddAsync(user);
            return _mapper.Map<UserDto>(user);
        }

        public async Task UpdatePasswordAsync(Guid id, UpdatePasswordAccountDto user)
        {
            var existingUser = await _userRepository.GetAsync(id);

            if (existingUser == null)
                throw new KeyNotFoundException();

            if (PasswordManager.ValidatePassword(user.Password, existingUser.Password))
            {
                throw new InvalidOperationException("The new password cannot be the same as the current password.");
            }

            existingUser.Password = PasswordManager.HashPassword(user.Password);

            await _userRepository.UpdateAsync(existingUser);
        }

        public async Task DeleteAsync(Guid id)
        {
            var existingUser = await _userRepository.GetAsync(id);

            if (existingUser == null)
                throw new KeyNotFoundException();

            await _userRepository.DeleteAsync(existingUser.Id);
        }
    }
}
