using CareGuide.Core.Interfaces;
using CareGuide.Data.TransactionManagement;
using CareGuide.Models.DTOs.Auth;
using CareGuide.Models.DTOs.Person;
using CareGuide.Models.DTOs.User;

namespace CareGuide.Core.Services
{
    public class AccountService : IAccountService
    {
        private readonly IUserService _userService;
        private readonly IPersonService _personService;
        private readonly IUnitOfWork _unitOfWork;

        public AccountService(IUserService userService, IPersonService personService, IUnitOfWork unitOfWork)
        {
            _userService = userService;
            _personService = personService;
            _unitOfWork = unitOfWork;
        }

        public AccountDto CreateAccount(CreateAccountDto createAccount)
        {
            _unitOfWork.BeginTransaction();

            try
            {
                PersonDto person = _personService.Create(new CreatePersonDto(createAccount));
                UserDto user = _userService.Create(person, new CreateUserDto(createAccount));

                _unitOfWork.CommitTransaction();

                return new AccountDto(user, person);
            }
            catch (Exception ex)
            {
                _unitOfWork.RollbackTransaction();
                throw new Exception(ex.Message);
            }
        }

        public void UpdatePasswordAccount(Guid id, UpdatePasswordAccountDto updatePasswordAccount)
        {
            _userService.UpdatePassword(id, updatePasswordAccount);
        }

        public void DeleteAccount(Guid id)
        {
            throw new NotImplementedException();
        }

        public AccountDto LoginAccount(LoginAccountDto loginAccount)
        {
            throw new NotImplementedException();
        }
    }
}
