using Microsoft.AspNetCore.Builder;
using Qrist.Interfaces;

namespace Qrist.Api.Host
{
    internal static class Mappings
    {
        internal static WebApplication MapHealthCheck(this WebApplication app)
        {
            app
                .MapGet("/healthcheck", async (IHealthChecker healthChecker) =>
                    await
                        healthChecker
                            .CheckHealthAsync()
                )
                .WithName("HealthCheck");

            return app;
        }
    }
}