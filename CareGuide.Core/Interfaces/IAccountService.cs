using CareGuide.Models.DTOs.Auth;

namespace CareGuide.Core.Interfaces
{
    public interface IAccountService
    {
        Task<AccountDto> CreateAccountAsync(CreateAccountDto createAccount, CancellationToken cancellationToken);
        Task<AccountDto> LoginAccountAsync(LoginAccountDto loginAccount, CancellationToken cancellationToken);
        Task UpdatePasswordAccountAsync(Guid id, UpdatePasswordAccountDto updatePasswordAccount, CancellationToken cancellationToken);
        Task DeleteAccountAsync(Guid id, CancellationToken cancellationToken);
    }
}
