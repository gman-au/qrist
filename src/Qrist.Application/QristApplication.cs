using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Qrist.Domain;
using Qrist.Interfaces;

namespace Qrist.Application
{
    public class QristApplication(
        IQrCodeGenerator qrCodeGenerator,
        IQrCodeEncoder qrCodeEncoder,
        IQrCodeProcessor qrCodeProcessor,
        ILogger<QristApplication> logger
    ) : IQristApplication
    {
        public async Task<string> ProduceQrCodeAsync(
            QrCodeRequest request,
            CancellationToken cancellationToken = default)
        {
            logger
                .LogInformation("Received produce QR code request");

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

        public async Task ProcessQrCodeAsync(
            string code,
            CancellationToken cancellationToken = default)
        {
            await
                qrCodeProcessor
                    .ProcessAsync(code, cancellationToken);
        }
    }
}