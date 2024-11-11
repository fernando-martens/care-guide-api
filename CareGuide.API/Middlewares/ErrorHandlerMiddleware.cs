using CareGuide.Models.Exceptions;
using FluentValidation;
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
                await HandleExceptionAsync(context, ex.Message, 401);
            }
            catch (InvalidOperationException ex)
            {
                await HandleExceptionAsync(context, ex.Message, 400);
            }
            catch (DbUpdateException ex)
            {
                await HandleExceptionAsync(context, ex.InnerException?.Message ?? ex.Message, 400);
            }
            catch (NotFoundException ex)
            {
                await HandleExceptionAsync(context, ex.InnerException?.Message ?? ex.Message, ex.StatusCode);
            }
            catch (ValidationException ex)
            {
                var errors = ex.Errors.Select(error => new
                {
                    Field = error.PropertyName,
                    Error = error.ErrorMessage
                });

                await HandleValidationExceptionAsync(context, errors, 400);
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

        private async Task HandleValidationExceptionAsync(HttpContext context, object errors, int statusCode = 400)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = statusCode;
            await context.Response.WriteAsJsonAsync(new
            {
                Message = "Validation failed",
                Errors = errors
            });
        }
    }
}
