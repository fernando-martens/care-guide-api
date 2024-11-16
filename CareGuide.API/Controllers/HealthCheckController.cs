using CareGuide.API.Attributes;
using CareGuide.Security.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CareGuide.API.Controllers
{
    public class HealthCheckController : BaseApiController
    {

        private readonly IUserSessionContext _userSessionContext;

        public HealthCheckController(ILogger<BaseApiController> _logger, IUserSessionContext userSessionContext) : base(_logger)
        {
            _userSessionContext = userSessionContext;
        }

        [HttpGet("Health")]
        [IgnoreSessionMiddleware]
        public ActionResult HealthStatus()
        {
            return Ok();
        }

    }
}
