using CareGuide.Core.Interfaces;
using CareGuide.Models.DTOs.Auth;
using CareGuide.Security.Interfaces;

namespace CareGuide.Tests.Context
{
    public class UserSessionContextTests : IUserSessionContext
    {
        public Guid UserId { get; set; }
        public Guid PersonId { get; set; }
        public string Email { get; set; }

        public UserSessionContextTests(IAccountService _accountService, IUserService _userService)
        {
            AccountDto account = _accountService.LoginAccountAsync(
                new LoginAccountDto("test123@domaintest.com.br", "Test123"),
                CancellationToken.None
            ).Result;

            UserId = account.Id;
            Email = account.Email;

            var userDto = _userService.GetByIdDtoAsync(account.Id, CancellationToken.None).Result;
            PersonId = userDto.PersonId;
        }
    }
}
