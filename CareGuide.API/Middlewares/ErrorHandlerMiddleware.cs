﻿using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace CareGuide.API.Middlewares
{
    public class ErrorHandlerMiddleware : IMiddleware
    {
        private static string PT(string slug) => $"https://problems-registry.smartbear.com/{slug}";
        private readonly ILogger<ErrorHandlerMiddleware> _logger;
        private readonly IWebHostEnvironment _env;

        public ErrorHandlerMiddleware(ILogger<ErrorHandlerMiddleware> logger, IWebHostEnvironment env)
        {
            _logger = logger;
            _env = env;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                HttpRequest request = context.Request;
                request.EnableBuffering();
                await next(context);
            }
            catch (ValidationException vex)
            {
                _logger.LogWarning(vex, "Validation error");
                await HandleValidationExceptionAsync(context, vex);
            }
            catch (DbUpdateConcurrencyException cex)
            {
                _logger.LogWarning(cex, "EF Core concurrency conflict");
                await HandleEfConcurrencyExceptionAsync(context, cex);
            }
            catch (DbUpdateException dbex)
            {
                _logger.LogWarning(dbex, "EF Core update exception");
                await HandleEfUpdateExceptionAsync(context, dbex);
            }
            catch (TimeoutException tex)
            {
                _logger.LogError(tex, "Operation timeout");
                await HandleTimeoutExceptionAsync(context, tex);
            }
            catch (KeyNotFoundException kex)
            {
                _logger.LogWarning(kex, "Key not found");
                await HandleNotFoundExceptionAsync(context, kex);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private ProblemDetails CreateProblemDetails(HttpContext context, int statusCode, string title, string? detail = null, string? type = null, IDictionary<string, object>? extensions = null)
        {
            var problem = new ProblemDetails
            {
                Type = type ?? "about:blank",
                Title = title,
                Status = statusCode,
                Detail = detail,
                Instance = context.Request.Path + context.Request.QueryString
            };

            if (_env.IsDevelopment())
            {
                problem.Extensions["traceId"] = context.TraceIdentifier;
            }

            if (extensions is not null)
            {
                foreach (var kvp in extensions)
                    problem.Extensions[kvp.Key] = kvp.Value;
            }

            return problem;
        }

        private Task HandleValidationExceptionAsync(HttpContext context, ValidationException vex)
        {
            context.Response.ContentType = "application/problem+json";
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;

            var errors = vex.Errors
                .GroupBy(e => e.PropertyName)
                .ToDictionary(g => g.Key, g => g.Select(e => e.ErrorMessage).ToArray());

            var problem = CreateProblemDetails(
                context,
                context.Response.StatusCode,
                title: "Validation failed.",
                detail: "One or more fields are invalid.",
                type: PT("validation-error"),
                extensions: new Dictionary<string, object> { ["errors"] = errors }
            );

            return context.Response.WriteAsJsonAsync(problem);
        }

        private Task HandleEfConcurrencyExceptionAsync(HttpContext context, DbUpdateConcurrencyException ex)
        {
            context.Response.ContentType = "application/problem+json";
            context.Response.StatusCode = (int)HttpStatusCode.Conflict;

            var extensions = _env.IsDevelopment()
                ? new Dictionary<string, object>
                {
                    ["entries"] = ex.Entries.Select(e => e.Entity.GetType().Name).ToArray(),
                    ["stackTrace"] = ex.StackTrace ?? string.Empty
                }
                : null;

            var problem = CreateProblemDetails(
                context,
                context.Response.StatusCode,
                title: "Concurrency conflict.",
                detail: "The resource was modified by another operation. Refresh and try again.",
                type: PT("business-rule-violation"),
                extensions: extensions
            );

            return context.Response.WriteAsJsonAsync(problem);
        }

        private Task HandleEfUpdateExceptionAsync(HttpContext context, DbUpdateException ex)
        {
            context.Response.ContentType = "application/problem+json";
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;

            var extensions = _env.IsDevelopment()
                ? new Dictionary<string, object> { ["stackTrace"] = ex.StackTrace ?? string.Empty }
                : null;

            var problem = CreateProblemDetails(
                context,
                context.Response.StatusCode,
                title: "Failed to persist changes.",
                detail: ex.Message,
                type: PT("server-error"),
                extensions: extensions
            );

            return context.Response.WriteAsJsonAsync(problem);
        }

        private Task HandleTimeoutExceptionAsync(HttpContext context, TimeoutException ex)
        {
            context.Response.ContentType = "application/problem+json";
            context.Response.StatusCode = 504;

            var extensions = _env.IsDevelopment()
                ? new Dictionary<string, object> { ["stackTrace"] = ex.StackTrace ?? string.Empty }
                : null;

            var problem = CreateProblemDetails(
                context,
                504,
                title: "Execution timeout exceeded.",
                detail: "The operation took longer than expected. Please try again.",
                type: PT("service-unavailable"),
                extensions: extensions
            );

            return context.Response.WriteAsJsonAsync(problem);
        }

        private Task HandleNotFoundExceptionAsync(HttpContext context, Exception ex)
        {
            context.Response.ContentType = "application/problem+json";
            context.Response.StatusCode = (int)HttpStatusCode.NotFound;

            var extensions = _env.IsDevelopment()
                ? new Dictionary<string, object> { ["stackTrace"] = ex.StackTrace ?? string.Empty }
                : null;

            var problem = CreateProblemDetails(
                context,
                context.Response.StatusCode,
                title: "Resource not found.",
                detail: ex.Message,
                type: PT("not-found"),
                extensions: extensions
            );

            return context.Response.WriteAsJsonAsync(problem);
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var statusCode = exception switch
            {
                ArgumentNullException => (int)HttpStatusCode.BadRequest,
                ArgumentException => (int)HttpStatusCode.BadRequest,
                KeyNotFoundException => (int)HttpStatusCode.NotFound,
                UnauthorizedAccessException => (int)HttpStatusCode.Unauthorized,
                HttpRequestException => (int)HttpStatusCode.BadRequest,
                InvalidOperationException => (int)HttpStatusCode.BadRequest,
                NotImplementedException => (int)HttpStatusCode.NotImplemented,
                _ => (int)HttpStatusCode.InternalServerError
            };

            context.Response.ContentType = "application/problem+json";
            context.Response.StatusCode = statusCode;

            LogException(exception, statusCode);

            var extensions = _env.IsDevelopment()
                ? new Dictionary<string, object> { ["stackTrace"] = exception.StackTrace ?? string.Empty }
                : null;

            string type = statusCode switch
            {
                400 => PT("bad-request"),
                401 => PT("unauthorized"),
                403 => PT("forbidden"),
                404 => PT("not-found"),
                503 => PT("service-unavailable"),
                500 => PT("server-error"),
                _ => "about:blank"
            };

            var problem = CreateProblemDetails(
                context,
                statusCode,
                title: "An unexpected error occurred.",
                detail: exception.Message,
                type: type,
                extensions: extensions
            );

            return context.Response.WriteAsJsonAsync(problem);
        }

        private void LogException(Exception ex, int statusCode)
        {
            if (statusCode >= 500)
                _logger.LogError(ex, "Internal server error: {Message}", ex.Message);
            else
                _logger.LogWarning(ex, "Request error: {Message}", ex.Message);
        }
    }
}
