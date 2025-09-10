using CareGuide.API.Attributes;
using CareGuide.Security.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CareGuide.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HealthCheckController : ControllerBase
    {
        private readonly IUserSessionContext _userSessionContext;

        public HealthCheckController(IUserSessionContext userSessionContext)
        {
            _userSessionContext = userSessionContext;
        }

        [HttpGet("HealthPrivate")]
        public IResult HealthPrivateStatus()
        {
            return Results.Ok(_userSessionContext.UserId);
        }

        [HttpGet("Health")]
        [IgnoreSessionMiddleware]
        public IResult HealthStatus()
        {
            return Results.NoContent();
        }

    }
}
