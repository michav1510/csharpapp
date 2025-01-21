using Microsoft.AspNetCore.Http;
using System.Diagnostics;

namespace CSharpApp.Application.Implementations
{
    public class LoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<LoggingMiddleware> _logger;

        public LoggingMiddleware(RequestDelegate next, ILogger<LoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Start the stopwatch to measure request time
            var stopwatch = Stopwatch.StartNew();

            try
            {
                // Call the next middleware in the pipeline
                await _next(context);
            }
            finally
            {
                // Stop the stopwatch
                stopwatch.Stop();

                // Log the performance details
                var elapsedMilliseconds = stopwatch.ElapsedMilliseconds;
                var requestPath = context.Request.Path;
                var method = context.Request.Method;
                var statusCode = context.Response.StatusCode;

                _logger.LogInformation("Request {Method} {Path} responded {StatusCode} in {ElapsedMilliseconds}ms",
                                        method, requestPath, statusCode, elapsedMilliseconds);
            }
        }
    }
}
