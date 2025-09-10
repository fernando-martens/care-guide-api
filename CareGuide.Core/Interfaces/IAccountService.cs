using CareGuide.Models.DTOs.Auth;

namespace CareGuide.Core.Interfaces
{
    public interface IAccountService
    {
        Task<AccountDto> CreateAccountAsync(CreateAccountDto createAccount);
        Task<AccountDto> LoginAccountAsync(LoginAccountDto loginAccount);
        Task UpdatePasswordAccountAsync(Guid id, UpdatePasswordAccountDto updatePasswordAccount);
        Task DeleteAccountAsync(Guid id);
    }
}
