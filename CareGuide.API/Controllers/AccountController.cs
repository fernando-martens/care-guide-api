using CareGuide.API.Attributes;
using CareGuide.Core.Interfaces;
using CareGuide.Models.DTOs.Auth;
using CareGuide.Models.DTOs.User;
using Microsoft.AspNetCore.Mvc;

namespace CareGuide.API.Controllers
{

    public class AccountController : BaseApiController
    {

        private readonly IAccountService _accountService;

        public AccountController(ILogger<BaseApiController> logger, IAccountService accountService) : base(logger)
        {
            _accountService = accountService;
        }

        [HttpPost("Create")]
        [IgnoreSessionMiddleware]
        public ActionResult<UserDto> Create([FromBody] CreateAccountDto createAccount)
        {
            return Ok(_accountService.CreateAccount(createAccount));
        }
         
        [HttpPost("Login")]
        [IgnoreSessionMiddleware]
        public ActionResult<UserDto> Login([FromBody] LoginAccountDto loginAccount)
        {
            return Ok(_accountService.LoginAccount(loginAccount));
        }

        [HttpPut("UpdatePassword/{id}")]
        public ActionResult<string> UpdatePassword(Guid id, [FromBody] UpdatePasswordAccountDto user)
        {
            _accountService.UpdatePasswordAccount(id, user);
            return Ok("Password changed successfully.");
        }

        [HttpDelete("DeleteAccount/{id}")]
        public ActionResult<string> DeleteAccount(Guid id)
        {
            _accountService.DeleteAccount(id);
            return Ok("Account successfully deleted.");
        }

    }
}
