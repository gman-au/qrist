using System;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Qrist.Domain;
using Qrist.Interfaces;

namespace Qrist.Infrastructure.QrCode.Encoding
{
    public class RequestEncoder(
        ICompressor compressor,
        ILogger<RequestEncoder> logger)
        : IRequestEncoder
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

            var encodedRequest =
                System.Text.Encoding
                    .Default
                    .GetBytes(jsonRequest);

            logger
                .LogDebug("Original request size: {size}", encodedRequest.Length);

            // compress

            encodedRequest =
                await
                    compressor
                        .CompressAsync(encodedRequest, cancellationToken);

            logger
                .LogDebug("Compressed request size: {size}", encodedRequest.Length);

            return encodedRequest;
        }
    }
}