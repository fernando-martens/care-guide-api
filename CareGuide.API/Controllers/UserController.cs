
using CareGuide.Core.Interfaces;
using CareGuide.Models.Tables;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CareGuide.API.Controllers
{

    public class UserController : BaseApiController
    {

        private readonly IUserService userService;

        public UserController(ILogger<BaseApiController> logger, IUserService _userService) : base(logger)
        {
            userService = _userService;
        }

        [HttpGet]
        public IActionResult List()
        {
            try
            {
                return Ok(userService.ListAll());
            }
            catch (Exception ex)
            {
                return HandleException(ex, ex.Message, 500);
            }
        }

        [HttpPost]
        public IActionResult Insert([FromBody] User user)
        {
            try
            {
                return Ok(userService.Insert(user));
            }
            catch (DbUpdateException dbEx)
            {
                return HandleException(dbEx, dbEx.InnerException?.Message ?? dbEx.Message, 400);
            }
            catch (Exception ex) {
                return HandleException(ex, ex.Message, 500);
            }
        }


    }
}
