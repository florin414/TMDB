using PopcornHub.Domain.DTOs;
using PopcornHub.Domain.DTOs.ApiError;

namespace PopcornHub.Web.Middlewares;

public class GlobalErrorHandlingMiddleware : IMiddleware
{
    private readonly ILogger<GlobalErrorHandlingMiddleware> _logger;

    public GlobalErrorHandlingMiddleware(ILogger<GlobalErrorHandlingMiddleware> logger)
    {
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unhandled exception");

            var (statusCode, message) = ex switch
            {
                ArgumentNullException => (StatusCodes.Status400BadRequest, "A required argument was null."),
                ArgumentException => (StatusCodes.Status400BadRequest, ex.Message),
                KeyNotFoundException => (StatusCodes.Status404NotFound, "The specified resource was not found."),
                UnauthorizedAccessException => (StatusCodes.Status401Unauthorized, "Unauthorized access."),
                _ => (StatusCodes.Status500InternalServerError, "An unexpected error occurred.")
            };

            var errorResponse = new ApiErrorResponse(statusCode, message);

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = statusCode;

            await context.Response.WriteAsJsonAsync(errorResponse);
        }
    }
}
