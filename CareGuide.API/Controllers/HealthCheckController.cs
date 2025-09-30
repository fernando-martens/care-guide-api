using CareGuide.Security.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

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


        [SwaggerOperation(Summary = "Health Check", Description = "Returns the health status of the API and the current user's ID.")]
        [HttpGet]
        public IResult HealthPrivateStatus()
        {
            return Results.Ok(_userSessionContext.UserId);
        }
    }
}
