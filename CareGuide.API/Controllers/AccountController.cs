using CareGuide.API.Attributes;
using CareGuide.API.Helpers;
using CareGuide.Core.Interfaces;
using CareGuide.Models.DTOs.Account;
using CareGuide.Models.DTOs.Auth;
using Microsoft.AspNetCore.Authorization;
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

        [HttpPost]
        [AllowAnonymous]
        [IgnoreSessionMiddleware]
        [SwaggerOperation(Summary = "Create Account", Description = "Creates a new user account.")]
        public async Task<IResult> Create([FromBody] CreateAccountDto createAccount, CancellationToken cancellationToken)
        {
            return Results.Ok(await _accountService.CreateAccountAsync(createAccount, cancellationToken));
        }

        [HttpPost("login")]
        [AllowAnonymous]
        [IgnoreSessionMiddleware]
        [SwaggerOperation(Summary = "Login", Description = "Authenticates a user and returns a JWT token.")]
        public async Task<IResult> Login([FromBody] LoginAccountDto loginAccount, CancellationToken cancellationToken)
        {
            var result = await _accountService.LoginAccountAsync(loginAccount, cancellationToken);

            var response = Results.Ok(new
            {
                result.Id,
                result.Email,
                result.SessionToken,
                result.Name,
                result.Gender,
                result.Birthday
            });

            AuthCookieHelper.AppendRefreshToken(HttpContext.Response, result.RefreshToken);

            return response;
        }

        [HttpPost("refresh")]
        [AllowAnonymous]
        [IgnoreSessionMiddleware]
        [SwaggerOperation(Summary = "Refresh Token", Description = "Refreshes JWT access token using a valid refresh token.")]
        public async Task<IResult> Refresh([FromBody] RefreshTokenDto refreshRequest, CancellationToken cancellationToken)
        {
            var result = await _accountService.RefreshTokenAsync(refreshRequest, cancellationToken);

            var response = Results.Ok(new
            {
                result.Id,
                result.Email,
                result.SessionToken,
                result.Name,
                result.Gender,
                result.Birthday
            });

            AuthCookieHelper.AppendRefreshToken(HttpContext.Response, result.RefreshToken);

            return response;
        }

        [HttpPut("{id}/password")]
        [SwaggerOperation(Summary = "Update Password", Description = "Updates the password for a specific user account.")]
        public async Task<IResult> UpdatePassword([FromRoute] Guid id, [FromBody] UpdatePasswordAccountDto user, CancellationToken cancellationToken)
        {
            await _accountService.UpdatePasswordAccountAsync(id, user, cancellationToken);
            return Results.NoContent();
        }

        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Delete Account", Description = "Deletes a user account by its ID.")]
        public async Task<IResult> DeleteAccount([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            await _accountService.DeleteAccountAsync(id, cancellationToken);
            return Results.NoContent();
        }

    }
}
