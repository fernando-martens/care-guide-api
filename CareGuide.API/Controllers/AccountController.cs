using CareGuide.API.Attributes;
using CareGuide.Core.Interfaces;
using CareGuide.Models.DTOs.Auth;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

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


        [SwaggerOperation(Summary = "Create Account", Description = "Creates a new user account.")]
        [HttpPost]
        [IgnoreSessionMiddleware]
        public async Task<IResult> Create([FromBody] CreateAccountDto createAccount, CancellationToken cancellationToken)
        {
            return Results.Ok(await _accountService.CreateAccountAsync(createAccount, cancellationToken));
        }


        [SwaggerOperation(Summary = "Login", Description = "Authenticates a user and returns a JWT token.")]
        [HttpPost("login")]
        [IgnoreSessionMiddleware]
        public async Task<IResult> Login([FromBody] LoginAccountDto loginAccount, CancellationToken cancellationToken)
        {
            return Results.Ok(await _accountService.LoginAccountAsync(loginAccount, cancellationToken));
        }


        [SwaggerOperation(Summary = "Update Password", Description = "Updates the password for a specific user account.")]
        [HttpPut("{id}/password")]
        public async Task<IResult> UpdatePassword([FromRoute] Guid id, [FromBody] UpdatePasswordAccountDto user, CancellationToken cancellationToken)
        {
            await _accountService.UpdatePasswordAccountAsync(id, user, cancellationToken);
            return Results.NoContent();
        }


        [SwaggerOperation(Summary = "Delete Account", Description = "Deletes a user account by its ID.")]
        [HttpDelete("{id}")]
        public async Task<IResult> DeleteAccount([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            await _accountService.DeleteAccountAsync(id, cancellationToken);
            return Results.NoContent();
        }

    }
}
