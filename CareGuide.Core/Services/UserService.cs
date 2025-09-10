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

        public List<UserDto> GetAll(int page = PaginationConstants.DefaultPage, int pageSize = PaginationConstants.DefaultPageSize)
        {
            List<User> list = _userRepository.GetAll(page, pageSize);
            return _mapper.Map<List<UserDto>>(list);
        }

        public UserDto GetByIdDto(Guid id)
        {
            User userTable = GetById(id);
            return _mapper.Map<UserDto>(userTable);
        }

        public User GetById(Guid id)
        {
            return _userRepository.Get(id) ?? throw new KeyNotFoundException();
        }

        public UserDto Create(PersonDto person, CreateUserDto createUser)
        {
            if (_userRepository.GetByEmail(createUser.Email) != null)
                throw new InvalidOperationException("Email already registered");

            User user = _mapper.Map<User>(createUser);
            _userRepository.Add(user);
            return _mapper.Map<UserDto>(user);
        }

        public void UpdatePassword(Guid id, UpdatePasswordAccountDto user)
        {
            User existingUser = _userRepository.Get(id) ?? throw new KeyNotFoundException();

            if (PasswordManager.ValidatePassword(user.Password, existingUser.Password))
            {
                throw new InvalidOperationException("The new password cannot be the same as the current password.");
            }

            existingUser.Password = PasswordManager.HashPassword(user.Password);

            _userRepository.Update(existingUser);
        }

        public void Delete(Guid id)
        {
            User existingUser = _userRepository.Get(id) ?? throw new KeyNotFoundException();
            _userRepository.Delete(existingUser.Id);
        }
    }
}
