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
            AccountDto account = _accountService.LoginAccount(new LoginAccountDto()
            {
                Email = "test123@domaintest.com.br",
                Password = "Test123"
            });

            UserId = account.Id;
        }
    }
}
