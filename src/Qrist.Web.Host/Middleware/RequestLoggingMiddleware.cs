using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Qrist.Web.Host.Middleware
{
    public class RequestLoggingMiddleware(RequestDelegate next, ILogger<RequestLoggingMiddleware> logger)
    {
        public async Task InvokeAsync(HttpContext context)
        {
            await LogRequest(context);

            await next(context);
        }

        private async Task LogRequest(HttpContext context)
        {
            try
            {
                // Enable buffering so we can read the body multiple times
                context.Request.EnableBuffering();

                // Read the request body
                var bodyAsText = await ReadRequestBody(context.Request);

                // Log all the details
                logger.LogInformation("=== INCOMING REQUEST ===");
                logger.LogInformation("Method: {Method}", context.Request.Method);
                logger.LogInformation("Path: {Path}", context.Request.Path);
                logger.LogInformation("Content-Type: {ContentType}", context.Request.ContentType);
                logger.LogInformation("Content-Length: {ContentLength}", context.Request.ContentLength);

                // Log headers
                logger.LogInformation("Headers:");
                foreach (var header in context.Request.Headers)
                {
                    logger.LogInformation("  {HeaderName}: {HeaderValue}", header.Key, string.Join(", ", header.Value.ToArray()));
                }

                // Log the raw body
                logger.LogInformation("Raw Body:");
                logger.LogInformation("{RequestBody}", bodyAsText);
                logger.LogInformation("=== END REQUEST ===");

                // Reset the stream position for the next middleware
                context.Request.Body.Position = 0;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error logging request");
            }
        }

        private static async Task<string> ReadRequestBody(HttpRequest request)
        {
            using var reader = new StreamReader(request.Body, Encoding.UTF8, leaveOpen: true);
            var body = await reader.ReadToEndAsync();
            request.Body.Position = 0; // Reset for next read
            return body;
        }
    }

// Extension method to make registration easier
    public static class RequestLoggingMiddlewareExtensions
    {
        public static IApplicationBuilder UseRequestLogging(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<RequestLoggingMiddleware>();
        }
    }
}