using System;
using System.Text;
using System.Threading;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http.HttpResults;
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
                        [FromServices] IQrCodeGenerator qrCodeGenerator,
                        [FromServices] IQrCodeEncoder qrCodeEncoder) =>
                    {
                        var cancellationToken = CancellationToken.None;

                        var qrCodeData =
                            await
                                qrCodeEncoder
                                    .ProcessAsync(request, cancellationToken);

                        var qrCodeString =
                            Convert
                                .ToBase64String(qrCodeData);

                        var qrCodeImage =
                            await
                                qrCodeGenerator
                                    .GenerateAsync(
                                        qrCodeString,
                                        cancellationToken
                                    );

                        return qrCodeImage;
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