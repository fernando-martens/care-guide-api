using CareGuide.API.Attributes;
using CareGuide.API.Helpers;
using CareGuide.Core.Interfaces;
using CareGuide.Models.DTOs.Account;
using CareGuide.Models.DTOs.Auth;
using CareGuide.Security.Interfaces;
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
            var result = await _accountService.CreateAccountAsync(createAccount, cancellationToken);

            var response = Results.Ok(new
            {
                result.Id,
                result.Email,
                result.Name,
                result.Gender,
                result.Birthday
            });

            AuthCookieHelper.AppendRefreshToken(HttpContext.Response, result.RefreshToken);
            AuthCookieHelper.AppendSessionToken(HttpContext.Response, result.SessionToken);

            return response;
        }

        [HttpPost("login")]
        [AllowAnonymous]
        [IgnoreSessionMiddleware]
        [SwaggerOperation(Summary = "Login", Description = "Authenticates a user and returns a JWT token and a refresh token.")]
        public async Task<IResult> Login([FromBody] LoginAccountDto loginAccount, CancellationToken cancellationToken)
        {
            var result = await _accountService.LoginAccountAsync(loginAccount, cancellationToken);

            var response = Results.Ok(new
            {
                result.Id,
                result.Email,
                result.Name,
                result.Gender,
                result.Birthday
            });

            AuthCookieHelper.AppendRefreshToken(HttpContext.Response, result.RefreshToken);
            AuthCookieHelper.AppendSessionToken(HttpContext.Response, result.SessionToken);

            return response;
        }

        [HttpPost("logout")]
        [SwaggerOperation(Summary = "Logout", Description = "Logs out the current user by revoking refresh tokens and clearing cookies.")]
        public async Task<IResult> Logout([FromServices] IUserSessionContext session, CancellationToken cancellationToken)
        {
            await _accountService.LogoutAccountAsync(session.UserId, cancellationToken);
            AuthCookieHelper.RemoveRefreshToken(HttpContext.Response);
            AuthCookieHelper.RemoveSessionToken(HttpContext.Response);

            return Results.NoContent();
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
                result.Name,
                result.Gender,
                result.Birthday
            });

            AuthCookieHelper.AppendRefreshToken(HttpContext.Response, result.RefreshToken);
            AuthCookieHelper.AppendSessionToken(HttpContext.Response, result.SessionToken);

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
