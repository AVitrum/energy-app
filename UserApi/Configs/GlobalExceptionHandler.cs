using System.Diagnostics;
using System.Security.Authentication;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using UserApi.Exceptions;

namespace UserApi.Configs;

public class GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        var traceId = Activity.Current?.Id ?? httpContext.TraceIdentifier;
        
        logger.LogError(
            exception,
            "Could not process a request on machine {MachineName}. TraceId: {TraceId}. Exception: {ExceptionMessage}. StackTrace: {StackTrace}",
            Environment.MachineName,
            traceId,
            exception.Message,
            exception.StackTrace
        );
        var (statusCode, title) = MapException(exception);

        await Results.Problem(
            title: title,
            statusCode: statusCode,
            extensions: new Dictionary<string, object?>
            {
                { "traceId", traceId }
            }
        ).ExecuteAsync(httpContext);

        return true;
    }

    private static (int StatusCode, string Title) MapException(Exception exception)
    {
        return exception switch
        {
            DbUpdateException { InnerException: Npgsql.PostgresException { SqlState: "23505" } } => (StatusCodes.Status409Conflict, exception.Message),
            ArgumentException => (StatusCodes.Status400BadRequest, exception.Message),
            AuthenticationException => (StatusCodes.Status401Unauthorized, exception.Message),
            NotImplementedException => (StatusCodes.Status501NotImplemented, exception.Message),
            UserExistsByEmailException => (StatusCodes.Status409Conflict, exception.Message),
            UserWrongPasswordException => (StatusCodes.Status401Unauthorized, exception.Message),
            _ when exception.GetType().IsGenericType && exception.GetType().GetGenericTypeDefinition() == typeof(
                EntityNotFoundException<>) => (StatusCodes.Status404NotFound, exception.Message),
            _ => (StatusCodes.Status500InternalServerError, "Internal Server Error")
        };
    }
}