using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Qrist.Domain;
using Qrist.Interfaces;

namespace Qrist.Api.Host.Infrastructure
{
    public class QrCodeBuilderRequestHandler(
        IEnumerable<IQristCodeBuilder> codeBuilders)
        : IQrCodeBuilderRequestHandler
    {
        private const int MaxQrCodeLengthBytes = 2953;

        public async Task<byte[]> HandleAsync(
            BuildQrCodeRequest request,
            CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrEmpty(request.Provider))
                throw new ArgumentException("Provider is required");

            var builderToUse =
                (codeBuilders ?? [])
                .FirstOrDefault(o => o.IsApplicable(request.Provider));

            if (builderToUse == null)
                throw new Exception($"No provider found for type \"{request.Provider}\".");

            var qrCode =
                await
                    builderToUse
                        .GenerateQrCodeAsync(request, cancellationToken);

            if (qrCode.Length > MaxQrCodeLengthBytes)
                throw new Exception("QR code data is too large - cannot create QR code.");

            return qrCode;
        }
    }
}