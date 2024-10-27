using CareGuide.Core.Interfaces;
using CareGuide.Models.DTOs.Auth;

namespace CareGuide.Core.Services
{
    public class AccountService : IAccountService
    {
        private readonly IUserService _userService;
        private readonly IPersonService _personService;

        public AccountService(IUserService userService, IPersonService personService) 
        {
            _userService = userService;
            _personService = personService;
        }

        public AccountDto CreateAccount(CreateAccountDto createAccount)
        {
            throw new NotImplementedException();
        }

        public void DeleteAccount(Guid id)
        {
            throw new NotImplementedException();
        }

        public AccountDto LoginAccount(LoginAccountDto loginAccount)
        {
            throw new NotImplementedException();
        }

        public AccountDto UpdatePasswordAccount(Guid id, UpdatePasswordAccountDto updatePasswordAccount)
        {
            throw new NotImplementedException();
        }
    }
}
