
using CareGuide.Core.Interfaces;
using CareGuide.Models.Tables;
using Microsoft.AspNetCore.Mvc;

namespace CareGuide.API.Controllers
{

    public class UserController : BaseApiController
    {

        private readonly IUserService userService;

        public UserController(IUserService _userService)
        {
            userService = _userService;
        }

        [HttpGet]
        public IActionResult List()
        {
            return Ok(userService.ListAll());
        }

        [HttpPost]
        public IActionResult Insert([FromBody] User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            return Ok(userService.Insert(user));
        }
    }
}
