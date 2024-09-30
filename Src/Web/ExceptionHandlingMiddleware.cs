using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Shortener.Application.Common.Exceptions;
using Shortener.Domain.Common.Exceptions.Base;

namespace Shortener.Web
{

    internal sealed class ExceptionHandlingMiddleware(ILogger<ExceptionHandlingMiddleware> logger) : IMiddleware
    {
        private readonly ILogger<ExceptionHandlingMiddleware> _logger = logger;

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);

                await HandleExceptionAsync(context, e);
            }
        }

        private static async Task HandleExceptionAsync(HttpContext httpContext, Exception exception)
        {
            httpContext.Response.ContentType = "application/json";

            httpContext.Response.StatusCode = exception switch
            {
                NotFoundException => StatusCodes.Status404NotFound,
                UnauthorizedAccessException => StatusCodes.Status401Unauthorized,
                ValidationException or BadRequestException  => StatusCodes.Status400BadRequest,
                _ => StatusCodes.Status500InternalServerError
            };

            ApiError[] errors = null;

            if (exception is ValidationException validationException)
            {
                errors = validationException.Errors
                    .SelectMany(
                        kvp => kvp.Value,
                        (kvp, value) => new ApiError(kvp.Key, value))
                    .ToArray();
            }

            var response = new Dictionary<string, object>
            {
                { "status", httpContext.Response.StatusCode },
                { "message", exception.Message }
            };

            if (errors != null && errors.Length > 0)
            {
                response["errors"] = errors;
            }

            await httpContext.Response.WriteAsync(JsonSerializer.Serialize(response));
        }

        private record ApiError(string PropertyName, string ErrorMessage);
    }
}