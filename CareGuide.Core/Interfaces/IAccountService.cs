using CareGuide.Models.DTOs.Auth;

namespace CareGuide.Core.Interfaces
{
    public interface IAccountService
    {
        AccountDto CreateAccount(CreateAccountDto createAccount);
        AccountDto LoginAccount(LoginAccountDto loginAccount);
        void UpdatePasswordAccount(Guid id, UpdatePasswordAccountDto updatePasswordAccount);
        void DeleteAccount(Guid id);
    }
}
