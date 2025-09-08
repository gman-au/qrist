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
        IQristUrlBuilder urlBuilder,
        IRequestEncoder requestEncoder,
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

            var encodedRequest =
                await
                    requestEncoder
                        .ProcessAsync(request, cancellationToken);

            var requestString =
                Convert
                    .ToBase64String(encodedRequest);

            var completeUrlString =
                urlBuilder
                    .BuildFullUrl(request.Provider, requestString);

            var qrCodeImage =
                await
                    qrCodeGenerator
                        .GenerateAsync(
                            completeUrlString,
                            cancellationToken
                        );

            return qrCodeImage;
        }

        public async Task<string> GetQrCodeActionConfirmationAsync(
            string code,
            CancellationToken cancellationToken = default) =>
            await
                qrCodeProcessor
                    .GetConfirmationAsync(code, cancellationToken);

        public async Task ProcessQrCodeActionAsync(
            string code,
            CancellationToken cancellationToken = default)
        {
            await
                qrCodeProcessor
                    .ProcessActionAsync(code, cancellationToken);
        }
    }
}