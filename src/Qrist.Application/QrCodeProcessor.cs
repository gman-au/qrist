using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Qrist.Domain;
using Qrist.Interfaces;

namespace Qrist.Application
{
    public class QrCodeProcessor(
        IRequestDecoder requestDecoder,
        IEnumerable<IRequestActioner> actioners,
        ILogger<QrCodeProcessor> logger
    ) : IQrCodeProcessor
    {
        public async Task<string> GetConfirmationAsync(
            string base64QrCode,
            CancellationToken cancellationToken = default
        )
        {
            var (request, actioner) =
                await
                    GetRequestAndActionerAsync(base64QrCode, cancellationToken);

            return
            await
                actioner
                    .GetConfirmationAsync(request, cancellationToken);
        }

        public async Task ProcessActionAsync(
            string base64QrCode,
            CancellationToken cancellationToken = default
        )
        {
            logger
                .LogInformation("Received QR code to process");

            var (request, actioner) =
                await
                    GetRequestAndActionerAsync(base64QrCode, cancellationToken);

            await
                actioner
                    .ProcessAsync(request, cancellationToken);

            logger
                .LogInformation("Processed QR code successfully");
        }

        private async Task<Tuple<QrCodeRequest, IRequestActioner>> GetRequestAndActionerAsync(
            string base64QrCode,
            CancellationToken cancellationToken = default)
        {
            var byteData =
                await
                    requestDecoder
                        .ProcessAsync(base64QrCode, cancellationToken);

            var request =
                JsonSerializer
                    .Deserialize<QrCodeRequest>(byteData);

            if (request.Provider == null)
                throw new Exception("Could not identify provider in QR code.");

            var actioner =
                actioners
                    .FirstOrDefault(o => o.IsApplicable(request.Provider));

            if (actioner == null)
                throw new Exception($"Could not identify actioner for QR code of provider type {request.Provider}.");

            return
                new Tuple<QrCodeRequest, IRequestActioner>(
                    request,
                    actioner
                );
        }
    }
}