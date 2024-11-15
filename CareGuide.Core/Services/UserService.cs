using AutoMapper;
using CareGuide.Core.Interfaces;
using CareGuide.Data.Interfaces;
using CareGuide.Models.DTOs.Auth;
using CareGuide.Models.DTOs.Person;
using CareGuide.Models.DTOs.User;
using CareGuide.Models.Exceptions;
using CareGuide.Models.Tables;
using CareGuide.Security;
using CareGuide.Security.Interfaces;

namespace CareGuide.Core.Services
{
    public class UserService : IUserService
    {

        private readonly IJwtService _jwtService;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserService(IJwtService jwtService, IUserRepository userRepository, IMapper mapper)
        {
            _jwtService = jwtService;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public List<UserDto> ListAll()
        {
            List<UserTable> list = _userRepository.ListAll();
            return _mapper.Map<List<UserDto>>(list);
        }

        public UserDto SelectByIdAsDto(Guid id)
        {
            UserTable userTable = SelectById(id);
            return _mapper.Map<UserDto>(userTable);
        }

        public UserTable SelectById(Guid id)
        {
            return _userRepository.SelectById(id) ?? throw new NotFoundException();
        }

        public UserDto Create(PersonDto person, CreateUserDto createUser)
        {
            if (_userRepository.SelectByEmail(createUser.Email) != null)
                throw new InvalidOperationException("Email already registered");

            UserTable user = _userRepository.Insert(new UserTable(person, createUser));
            return _mapper.Map<UserDto>(user);
        }

        public void UpdatePassword(Guid id, UpdatePasswordAccountDto user)
        {
            UserTable existingUser = _userRepository.SelectById(id) ?? throw new NotFoundException();

            if (PasswordManager.ValidatePassword(user.Password, existingUser.Password))
            {
                throw new InvalidOperationException("The new password cannot be the same as the current password.");
            }

            existingUser.Password = PasswordManager.HashPassword(user.Password);
            existingUser.Register = DateTime.UtcNow;

            _userRepository.Update(existingUser);
        }

        public void Delete(Guid id)
        {
            UserTable existingUser = _userRepository.SelectById(id) ?? throw new NotFoundException();
            _userRepository.Remove(existingUser);
        }

    }

}
