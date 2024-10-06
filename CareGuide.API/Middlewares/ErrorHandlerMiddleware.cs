using Microsoft.EntityFrameworkCore;


namespace CareGuide.API.Middlewares
{
    public class ErrorHandlerMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                HttpRequest request = context.Request;
                request.EnableBuffering();
                await next(context);
            }
            catch (UnauthorizedAccessException ex)
            {
                return HandleExceptionAsync(context, ex.Message, 401);
            }
            catch (InvalidOperationException ex)
            {
                await HandleExceptionAsync(context, ex.Message, 400);
            }
            catch (DbUpdateException ex)
            {
                await HandleExceptionAsync(context, ex.InnerException?.Message ?? ex.Message, 400);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex.Message, 500);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, string message, int statusCode = 500)
        {
            context.Response.ContentType = "application/text";
            context.Response.StatusCode = statusCode;
            await context.Response.WriteAsync(message);
        }

    }
}
