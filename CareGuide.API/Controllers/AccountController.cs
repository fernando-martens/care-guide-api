using CareGuide.API.Attributes;
using CareGuide.Core.Interfaces;
using CareGuide.Models.DTOs.Auth;
using Microsoft.AspNetCore.Mvc;

namespace CareGuide.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost]
        [IgnoreSessionMiddleware]
        public IResult Create([FromBody] CreateAccountDto createAccount)
        {
            return Results.Ok(_accountService.CreateAccount(createAccount));
        }

        [HttpPost("login")]
        [IgnoreSessionMiddleware]
        public IResult Login([FromBody] LoginAccountDto loginAccount)
        {
            return Results.Ok(_accountService.LoginAccount(loginAccount));
        }

        [HttpPut("{id}/password")]
        public IResult UpdatePassword(Guid id, [FromBody] UpdatePasswordAccountDto user)
        {
            _accountService.UpdatePasswordAccount(id, user);
            return Results.NoContent();
        }

        [HttpDelete("{id}")]
        public IResult DeleteAccount(Guid id)
        {
            _accountService.DeleteAccount(id);
            return Results.NoContent();
        }

    }
}
