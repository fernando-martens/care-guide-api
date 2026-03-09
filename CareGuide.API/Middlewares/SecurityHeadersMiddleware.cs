using Microsoft.Net.Http.Headers;

namespace CareGuide.API.Middlewares
{
    public sealed class SecurityHeadersMiddleware
    {
        private readonly RequestDelegate _next;

        public SecurityHeadersMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            context.Response.OnStarting(() =>
            {
                var headers = context.Response.Headers;

                headers[HeaderNames.XContentTypeOptions] = "nosniff";
                headers["Referrer-Policy"] = "no-referrer";
                headers[HeaderNames.XFrameOptions] = "DENY";

                return Task.CompletedTask;
            });

            await _next(context);
        }
    }
}