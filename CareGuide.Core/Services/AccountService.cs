using CareGuide.Core.Interfaces;
using CareGuide.Data.Interfaces;
using CareGuide.Data.TransactionManagement;
using CareGuide.Models.DTOs.Auth;
using CareGuide.Models.DTOs.Person;
using CareGuide.Models.DTOs.User;
using CareGuide.Models.Tables;
using CareGuide.Security;
using CareGuide.Security.Interfaces;

namespace CareGuide.Core.Services
{
    public class AccountService : IAccountService
    {
        private readonly IUserRepository _userRepository;

        private readonly IUserService _userService;
        private readonly IPersonService _personService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IJwtService _jwtService;

        public AccountService(IUserService userService, IUserRepository userRepository, IPersonService personService, IUnitOfWork unitOfWork, IJwtService jwtService)
        {
            _userService = userService;
            _userRepository = userRepository;
            _personService = personService;
            _unitOfWork = unitOfWork;
            _jwtService = jwtService;
        }

        public AccountDto CreateAccount(CreateAccountDto createAccount)
        {
            _unitOfWork.BeginTransaction();

            try
            {
                PersonDto person = _personService.Create(new CreatePersonDto(createAccount));
                UserDto user = _userService.Create(person, new CreateUserDto(createAccount));

                string token = _jwtService.GenerateToken(user.Id, user.Email);

                _unitOfWork.CommitTransaction();

                return new AccountDto(user, person, token);
            }
            catch (Exception ex)
            {
                _unitOfWork.RollbackTransaction();
                throw new Exception(ex.Message);
            }
        }

        public AccountDto LoginAccount(LoginAccountDto loginAccount)
        {
            User? user = _userRepository.GetByEmail(loginAccount.Email);

            if (user == null || !PasswordManager.ValidatePassword(loginAccount.Password, user.Password))
                throw new InvalidOperationException("wrong password or email");

            string token = _jwtService.GenerateToken(user.Id, loginAccount.Email);

            UserDto userDto = _userService.GetByIdDto(user.Id);
            PersonDto personDto = _personService.Get(userDto.PersonId);

            return new AccountDto(userDto, personDto, token);
        }

        public void UpdatePasswordAccount(Guid id, UpdatePasswordAccountDto updatePasswordAccount)
        {
            _userService.UpdatePassword(id, updatePasswordAccount);
        }

        public void DeleteAccount(Guid id)
        {
            throw new NotImplementedException();
        }

    }
}
