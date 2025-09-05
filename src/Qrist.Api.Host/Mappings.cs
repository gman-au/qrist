using System;
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
                        [FromServices] IQrCodeEncoder qrCodeEncoder) =>
                    {
                        var qrCode =
                            await
                                qrCodeEncoder
                                    .ProcessAsync(request);

                        var qrCodeString =
                            Convert
                                .ToBase64String(qrCode);

                        return qrCodeString;

                        return
                            Uri
                                .EscapeDataString(qrCodeString);
                    }
                )
                .WithName("BuildCode");

            return app;
        }

        internal static WebApplication MapQrCodeProcessorRequests(this WebApplication app)
        {
            app
                .MapPost("/ProcessCode", async (
                        [FromQuery] string code,
                        [FromServices] IQrCodeProcessor qrCodeProcessor) =>
                    {
                        await
                            qrCodeProcessor
                                .ProcessAsync(code);
                    }
                )
                .WithName("ProcessCode");

            return app;
        }
    }
}