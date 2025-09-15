using CareGuide.Security.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CareGuide.API.Controllers
{
    [ApiController]
    [Route(CareGuide.Models.Constants.ApiConstants.VersionPrefix + "/[controller]")]
    public class HealthCheckController : ControllerBase
    {
        private readonly IUserSessionContext _userSessionContext;

        public HealthCheckController(IUserSessionContext userSessionContext)
        {
            _userSessionContext = userSessionContext;
        }

        [HttpGet]
        public IResult HealthPrivateStatus()
        {
            return Results.Ok(_userSessionContext.UserId);
        }
    }
}
