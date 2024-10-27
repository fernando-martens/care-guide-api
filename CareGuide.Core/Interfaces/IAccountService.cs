using CareGuide.Models.DTOs.Auth;
using CareGuide.Models.DTOs.Person;
using CareGuide.Models.Tables;

namespace CareGuide.Core.Interfaces
{
    public interface IAccountService
    {
        AccountDto CreateAccount(CreateAccountDto createAccount);
        AccountDto LoginAccount(LoginAccountDto loginAccount);
        AccountDto UpdatePasswordAccount(Guid id, UpdatePasswordAccountDto updatePasswordAccount);
        void DeleteAccount(Guid id);
    }
}
