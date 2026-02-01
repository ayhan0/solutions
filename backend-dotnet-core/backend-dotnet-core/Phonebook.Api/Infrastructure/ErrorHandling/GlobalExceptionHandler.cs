using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace Phonebook.Api.Infrastructure.ErrorHandling;

public sealed class GlobalExceptionHandler : IExceptionHandler
{
    private readonly ILogger<GlobalExceptionHandler> _logger;
    private readonly IHostEnvironment _env;

    public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger, IHostEnvironment env)
    {
        _logger = logger;
        _env = env;
    }

    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        var traceId = httpContext.TraceIdentifier;

        // Default mapping
        var (statusCode, title) = Map(exception);

        // Log
        if (statusCode >= 500)
            _logger.LogError(exception, "Unhandled exception. traceId={TraceId}", traceId);
        else
            _logger.LogInformation(exception, "Handled exception. traceId={TraceId}", traceId);

        var problem = new ProblemDetails
        {
            Status = statusCode,
            Title = title,
            Type = $"https://httpstatuses.com/{statusCode}",
            Instance = httpContext.Request.Path
        };

        // Extra fields
        problem.Extensions["traceId"] = traceId;

        // Dev ortamında detay ver, prod’da verme
        if (_env.IsDevelopment())
        {
            problem.Detail = exception.Message;
            problem.Extensions["exception"] = exception.GetType().Name;
        }

        httpContext.Response.StatusCode = statusCode;
        httpContext.Response.ContentType = "application/problem+json";

        await httpContext.Response.WriteAsJsonAsync(problem, cancellationToken);
        return true; // exception handled
    }

    private static (int statusCode, string title) Map(Exception ex) =>
        ex switch
        {
            ValidationException => (StatusCodes.Status400BadRequest, "Validation failed"),
            ArgumentException => (StatusCodes.Status400BadRequest, "Bad request"),
            KeyNotFoundException => (StatusCodes.Status404NotFound, "Not found"),
            UnauthorizedAccessException => (StatusCodes.Status401Unauthorized, "Unauthorized"),
            _ => (StatusCodes.Status500InternalServerError, "Internal server error")
        };
}
