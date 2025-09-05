using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Qrist.Domain;
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

        internal static WebApplication MapQrCodeBuilderRequest(this WebApplication app)
        {
            app
                .MapPost("/BuildCode", async (
                        [FromBody] BuildQrCodeRequest request,
                        [FromServices] IQrCodeBuilderRequestHandler qrCodeBuilderRequestHandler) =>
                    await
                        qrCodeBuilderRequestHandler
                            .HandleAsync(request)
                )
                .WithName("BuildCode");

            return app;
        }
    }
}