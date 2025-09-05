using System.Threading;
using System.Threading.Tasks;
using QRCoder;
using Qrist.Interfaces;

namespace Qrist.Infrastructure.QrCode.Production
{
    public class QrCodeGenerator : IQrCodeGenerator
    {
        public async Task<string> GenerateAsync(
            string value,
            CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrEmpty(value))
                return null;

            using var qrGenerator = new QRCodeGenerator();

            using var qrCodeData =
                qrGenerator
                    .CreateQrCode(
                        value,
                        QRCodeGenerator.ECCLevel.Q
                    );

            using var qrCode = new Base64QRCode(qrCodeData);

            var qrCodeImage =
                qrCode
                    .GetGraphic(20);

            return qrCodeImage;
        }
    }
}