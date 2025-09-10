using CareGuide.API.Attributes;
using CareGuide.Core.Interfaces;
using CareGuide.Models.DTOs.Auth;
using Microsoft.AspNetCore.Mvc;

namespace CareGuide.API.Controllers
{
    [ApiController]
    [Route(CareGuide.Models.Constants.ApiConstants.VersionPrefix + "/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost]
        [IgnoreSessionMiddleware]
        public async Task<IResult> Create([FromBody] CreateAccountDto createAccount)
        {
            return Results.Ok(await _accountService.CreateAccountAsync(createAccount));
        }

        [HttpPost("login")]
        [IgnoreSessionMiddleware]
        public async Task<IResult> Login([FromBody] LoginAccountDto loginAccount)
        {
            return Results.Ok(await _accountService.LoginAccountAsync(loginAccount));
        }

        [HttpPut("{id}/password")]
        public async Task<IResult> UpdatePassword(Guid id, [FromBody] UpdatePasswordAccountDto user)
        {
            await _accountService.UpdatePasswordAccountAsync(id, user);
            return Results.NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IResult> DeleteAccount(Guid id)
        {
            await _accountService.DeleteAccountAsync(id);
            return Results.NoContent();
        }

    }
}
