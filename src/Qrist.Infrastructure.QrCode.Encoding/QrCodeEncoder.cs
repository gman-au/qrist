using System;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Qrist.Domain;
using Qrist.Interfaces;

namespace Qrist.Infrastructure.QrCode.Encoding
{
    public class QrCodeEncoder(
        ICompressor compressor,
        ILogger<QrCodeEncoder> logger)
        : IQrCodeEncoder
    {
        private const int MaxQrCodeLengthBytes = 2953;

        public async Task<byte[]> ProcessAsync(
            QrCodeRequest request,
            CancellationToken cancellationToken = default)
        {

            if (request == null)
                throw new Exception("Request contains no data.");

            var jsonRequest =
                JsonSerializer
                    .Serialize(request);

            // minify?

            var qrCode =
                System.Text.Encoding
                    .Default
                    .GetBytes(jsonRequest);

            logger
                .LogDebug("Original request size: {size}", qrCode.Length);

            // compress

            qrCode =
                await
                    compressor
                        .CompressAsync(qrCode, cancellationToken);

            logger
                .LogDebug("Compressed request size: {size}", qrCode.Length);

            if (qrCode.Length > MaxQrCodeLengthBytes)
                throw new Exception("QR code data is too large - cannot create QR code.");

            return qrCode;
        }
    }
}