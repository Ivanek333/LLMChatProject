using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Shared.Application.Exceptions;
using System.Text.Json;
using ApplicationException = Shared.Application.Exceptions.ApplicationException;

namespace Shared.Application.Middleware
{
    public class ExceptionInterceptorMiddleware(ILogger<ExceptionInterceptorMiddleware> logger) : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception e)
            {
                logger.LogError(e, e.Message);
                await HandleExceptionAsync(context, e);
            }
        }

        static async Task HandleExceptionAsync(HttpContext context, Exception e)
        {
            var statusCode = GetStatusCode(e);
            var response = new
            {
                title = GetTitle(e),
                status = statusCode,
                detail = e.Message,
                errors = GetErrors(e)
            };

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = statusCode;
            await context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }

        static int GetStatusCode(Exception e)
            => e switch
            {
                ValidationException => StatusCodes.Status422UnprocessableEntity,
                NotFoundException => StatusCodes.Status404NotFound,
                UnauthorizedException => StatusCodes.Status401Unauthorized,
                _ => StatusCodes.Status500InternalServerError
            };
        static string GetTitle(Exception e)
            => e switch
            {
                ApplicationException ae => ae.Title,
                _ => "Server Error"
            };
        static IReadOnlyDictionary<string, string[]>? GetErrors(Exception e)
            => e switch
            {
                ValidationException ve => ve.Errors,
                _ => null
            };
    }
}
