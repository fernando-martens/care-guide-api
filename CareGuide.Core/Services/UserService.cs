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
        private readonly IUserRepository userRepository;

        public UserService(IJwtService jwtService, IUserRepository _userRepository)
        {
            _jwtService = jwtService;
            userRepository = _userRepository;
        }

        public List<User> ListAll()
        {
            return userRepository.ListAll();
        }

        public User ListById(Guid id)
        {
            return userRepository.ListById(id);
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

            return userRepository.Insert(userToCreate);
        }

        public void UpdatePassword(Guid id, UserUpdatePasswordDto user)
        {
            User userToUpdate = new User
            {
                Id = id,
                Register = DateTime.UtcNow,
                Password = user.Password
            };

            userRepository.UpdatePassword(userToUpdate);
        }

        public void Remove(Guid id)
        {
            userRepository.Remove(id);
        }

        public User Login(UserRequestDto user)
        {
            User userValidated = ValidateUser(user.Email, user.Password);

            if (userValidated == null)
            {
                throw new UnauthorizedAccessException("Invalid credentials");
            }

            string token = _jwtService.GenerateToken(userValidated.Id, userValidated.Email);
            userValidated.Register = DateTime.UtcNow;

            return userRepository.UpdateSessionToken(userValidated, token);
        }

        private User ValidateUser(string email, string password)
        {
            User user = userRepository.ListByEmail(email);

            if (user == null || !PasswordManager.ValidatePassword(password, user.Password))
            {
                return null;
            }

            return user;
        }

    }

}
