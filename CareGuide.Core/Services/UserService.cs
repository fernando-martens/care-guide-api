using CareGuide.Core.Interfaces;
using CareGuide.Data.Interfaces;
using CareGuide.Models.DTOs.User;
using CareGuide.Models.Tables;
using CareGuide.Security;
using CareGuide.Security.Interfaces;

namespace CareGuide.Core.Services
{
    public class UserService : IUserService
    {

        private readonly IJwtService _jwtService;
        private readonly IUserRepository _userRepository;

        public UserService(IJwtService jwtService, IUserRepository userRepository)
        {
            _jwtService = jwtService;
            _userRepository = userRepository;
        }

        public List<User> ListAll()
        {
            return _userRepository.ListAll();
        }

        public User ListById(Guid id)
        {
            return _userRepository.ListById(id);
        }

        public User Insert(UserRequestDto user)
        {
            User userToCreate = new User
            {
                Id = Guid.NewGuid(),
                Register = DateTime.UtcNow,
                Email = user.Email,
                Password = PasswordManager.HashPassword(user.Password)
            };

            return _userRepository.Insert(userToCreate);
        }

        public void UpdatePassword(Guid id, UserUpdatePasswordDto user)
        {
            User existingUser = _userRepository.ListById(id);

            if (PasswordManager.ValidatePassword(user.Password, existingUser.Password))
            {
                throw new InvalidOperationException("The new password cannot be the same as the current password.");
            }

            existingUser.Password = PasswordManager.HashPassword(user.Password);
            existingUser.Register = DateTime.UtcNow;

            _userRepository.Update(existingUser);
        }

        public void Remove(Guid id)
        {
            User existingUser = _userRepository.ListById(id);
            _userRepository.Remove(existingUser);
        }

        public User Login(UserRequestDto user)
        {
            User existingUser = _userRepository.ListByEmail(user.Email);

            if (existingUser == null || !PasswordManager.ValidatePassword(user.Password, existingUser.Password))
            {
                throw new UnauthorizedAccessException("Invalid credentials");
            }

            existingUser.SessionToken = _jwtService.GenerateToken(existingUser.Id, existingUser.Email);
            existingUser.Register = DateTime.UtcNow;

            return _userRepository.Update(existingUser);
        }
    }

}
