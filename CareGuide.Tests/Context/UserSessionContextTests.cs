using CareGuide.Core.Interfaces;
using CareGuide.Models.DTOs.Auth;
using CareGuide.Security.Interfaces;

namespace CareGuide.Tests.Context
{
    public class UserSessionContextTests : IUserSessionContext
    {
        public Guid UserId { get; set; }

        public UserSessionContextTests(IAccountService _accountService)
        {
            AccountDto account = _accountService.LoginAccountAsync(
                new LoginAccountDto("test123@domaintest.com.br", "Test123"),
                CancellationToken.None
            ).Result;

            UserId = account.Id;
        }
    }
}
