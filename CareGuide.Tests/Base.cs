using CareGuide.Core.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace CareGuide.Tests
{
    [Collection("Startup")]
    public class Base
    {

        public readonly IAccountService _accountService;

        public Base(Program program)
        {
            _accountService = program.serviceProvider.GetRequiredService<IAccountService>();
        }
    }
}
