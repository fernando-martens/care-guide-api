using CareGuide.API.Endpoints.Shared;
using CareGuide.API.Extensions;
using CareGuide.Security.Interfaces;

namespace CareGuide.API.Endpoints;

public class HealthCheckEndpoints() : IEndpoint
{
    public void RegisterEndpoints(IEndpointRouteBuilder endpoints)
    {
        var group = endpoints
            .MapGroup("/health-check")
            .WithTags("Health Checks")
            .WithDefaultProblemResponses();

        group.MapGet("/", HealthPrivateStatus)
             .WithSummary("Health Check")
             .WithDescription("Returns the health status of the API and the current user's ID.")
             .Produces<Guid>(StatusCodes.Status200OK);
    }

    private static IResult HealthPrivateStatus(IUserSessionContext userSessionContext)
    {
        return Results.Ok(userSessionContext.UserId);
    }
}