using Microsoft.AspNetCore.Mvc;

namespace CareGuide.API.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class BaseApiController : ControllerBase
    {

        protected readonly ILogger<BaseApiController> logger;

        public BaseApiController(ILogger<BaseApiController> _logger)
        {
            logger = _logger;
        }

        protected IActionResult HandleException(Exception ex, string exMessage, int statusCode)
        {
            logger.LogError(ex, "An error occurred.");
            return StatusCode(statusCode, exMessage);
        }

    }
}
