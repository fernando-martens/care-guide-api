using CareGuide.API.Attributes;
using CareGuide.Security.Interfaces;
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
            if (context.GetEndpoint()?.Metadata.GetMetadata<IgnoreSessionMiddleware>() != null)
            {
                await next(context);
                return;
            }

            var path = context.Request.Path.Value?.ToLower();
            if (path != null && (path.StartsWith("/scalar") || path.StartsWith("/api-reference") || path.StartsWith("/api-docs") || path.StartsWith("/openapi")))
            {
                await next(context);
                return;
            }

            if (context.Request.Headers.TryGetValue("Authorization", out StringValues headerAuth))
            {
                if (_jwtService.ValidateToken(headerAuth.ToString().Replace("Bearer ", "")) == null)
                    throw new UnauthorizedAccessException("invalid token");
            }
            else
            {
                throw new UnauthorizedAccessException("invalid token");
            }

            await next(context);
        }
    }
}
