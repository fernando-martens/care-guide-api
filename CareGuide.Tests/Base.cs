using CareGuide.Core.Interfaces;
using CareGuide.Security.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace CareGuide.Tests
{
    [Collection("Startup")]
    public class Base
    {

        public readonly IAccountService _accountService;
        public readonly IUserSessionContext _userSessionContext;

        public Base(Program program)
        {
            _accountService = program.serviceProvider.GetRequiredService<IAccountService>();
            _userSessionContext = program.serviceProvider.GetRequiredService<IUserSessionContext>();
        }
    }
}
