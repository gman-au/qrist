using System.Threading;
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

        internal static WebApplication MapQrCodeBuilderRequests(this WebApplication app)
        {
            app
                .MapPost("/BuildCode", async (
                        [FromBody] QrCodeRequest request,
                        [FromServices] IQristApplication qristApplication) =>
                    {
                        var cancellationToken = CancellationToken.None;

                        return
                            await
                                qristApplication
                                    .ProduceQrCodeAsync(request, cancellationToken);
                    }
                )
                .WithName("BuildCode");

            return app;
        }

        internal static WebApplication MapUrlBuilderRequests(this WebApplication app)
        {
            app
                .MapPost("/BuildUrl", async (
                        [FromBody] QrCodeRequest request,
                        [FromServices] IQristApplication qristApplication) =>
                    {
                        var cancellationToken = CancellationToken.None;

                        return
                            await
                                qristApplication
                                    .ProduceFullRequestUrlAsync(request, cancellationToken);
                    }
                )
                .WithName("BuildUrl");

            return app;
        }
    }
}