using System;
using System.Threading;
using System.Threading.Tasks;
using Qrist.Interfaces;

namespace Qrist.Infrastructure.QrCode.Encoding
{
    public class QrCodeDecoder(ICompressor compressor)
        : IQrCodeDecoder
    {
        public async Task<byte[]> ProcessAsync(
            string base64QrCode,
            CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrEmpty(base64QrCode))
                throw new Exception("Request contains no data.");

            // decompress

            var byteData =
                await
                    compressor
                        .DecompressAsync(
                            Convert
                                .FromBase64String(base64QrCode),
                            cancellationToken
                        );

            return byteData;
        }
    }
}