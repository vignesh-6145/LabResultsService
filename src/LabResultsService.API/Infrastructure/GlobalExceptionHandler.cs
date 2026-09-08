using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace LabResultsService.API.Infrastructure
{
    public class GlobalExceptionHandler(IProblemDetailsService _problemDetailsService, ILogger<GlobalExceptionHandler> _logger) : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            _logger.LogError(exception, "Unhandled exception occurred");
            httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError; // the rest were handled in the controller

            return await _problemDetailsService.TryWriteAsync(new ProblemDetailsContext {
                HttpContext = httpContext,
                Exception = exception,
                ProblemDetails = new ProblemDetails
                {
                    Type = exception.GetType().Name,
                    Title = "An error occurred",
                    Detail = exception.Message
                }
            });
        }
    }
}
