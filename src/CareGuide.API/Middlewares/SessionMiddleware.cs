using CareGuide.Security.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Primitives;

namespace CareGuide.API.Middlewares
{
    public class SessionMiddleware : IMiddleware
    {
        private readonly IJwtService _jwtService;

        public SessionMiddleware(IJwtService jwtService)
        {
            _jwtService = jwtService;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            var endpoint = context.GetEndpoint();

            if (endpoint?.Metadata.GetMetadata<IgnoreSessionMiddleware>() is not null || endpoint?.Metadata.GetMetadata<IAllowAnonymous>() is not null)
            {
                await next(context);
                return;
            }

            var path = context.Request.Path.Value?.ToLowerInvariant();
            if (path is not null &&
                (path.StartsWith("/scalar") ||
                 path.StartsWith("/api-reference") ||
                 path.StartsWith("/api-docs") ||
                 path.StartsWith("/openapi")))
            {
                await next(context);
                return;
            }

            if (!context.Request.Headers.TryGetValue("Authorization", out StringValues headerAuth))
            {
                throw new UnauthorizedAccessException("invalid token");
            }

            var authorizationHeader = headerAuth.ToString();

            if (!authorizationHeader.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
            {
                throw new UnauthorizedAccessException("invalid token");
            }

            var token = authorizationHeader["Bearer ".Length..].Trim();

            if (string.IsNullOrWhiteSpace(token) || _jwtService.ValidateToken(token) is null)
            {
                throw new UnauthorizedAccessException("invalid token");
            }

            await next(context);
        }
    }
}