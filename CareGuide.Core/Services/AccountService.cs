using CareGuide.Core.Interfaces;
using CareGuide.Models.DTOs.Auth;
using CareGuide.Models.DTOs.Person;
using CareGuide.Models.DTOs.User;

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
            // To-do: rollback in case of error

            PersonDto person = _personService.Create(new CreatePersonDto(createAccount));
            UserDto user = _userService.Create(person, new CreateUserDto(createAccount));

            return new AccountDto(user, person);
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
