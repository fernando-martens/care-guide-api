using CareGuide.Core.Interfaces;
using CareGuide.Data.Interfaces;
using CareGuide.Models.Tables;
using CareGuide.Security;

namespace CareGuide.Core.Services
{
    public class UserService : IUserService
    {

        private readonly IUserRepository userRepository;

        public UserService(IUserRepository _userRepository)
        {
            userRepository = _userRepository;
        }

        public Guid Insert(User user)
        {
            var userCreated = new User
            {
                Id = Guid.NewGuid(),
                Register = DateTime.UtcNow,
                Email = user.Email,
                Password = PasswordManager.HashPassword(user.Password)
            };

            userRepository.Insert(userCreated);

            return userCreated.Id;
        }

        public List<User> ListAll()
        {
            return userRepository.ListAll();
        }

    }

}
