using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Qrist.Domain;
using Qrist.Interfaces;

namespace Qrist.Api.Host.Infrastructure
{
    public class QrCodeProcessor(
        IQrCodeDecoder qrCodeDecoder,
        IEnumerable<IRequestActioner> actioners,
        ILogger<QrCodeProcessor> logger
    ) : IQrCodeProcessor
    {
        public async Task ProcessAsync(
            string base64QrCode,
            CancellationToken cancellationToken = default
        )
        {
            var byteData =
                await
                    qrCodeDecoder
                        .ProcessAsync(base64QrCode, cancellationToken);

            var request =
                JsonSerializer
                    .Deserialize<QrCodeRequest>(byteData);

            if (request.Provider == null)
                throw new Exception("Could not identify provider in QR code.");

            var processor =
                actioners
                    .FirstOrDefault(o => o.IsApplicable(request.Provider));

            if (processor == null)
                throw new Exception($"Could not identify processor for QR code of provider type {request.Provider}.");

            await
                processor
                    .ProcessAsync(request, cancellationToken);
        }
    }
}