using System.IO;
using System.IO.Compression;
using System.Threading;
using System.Threading.Tasks;
using Qrist.Interfaces;

namespace Qrist.Infrastructure.Gzip
{
    public class GzipCompressor : ICompressor
    {
        public async Task<byte[]> CompressAsync(
            byte[] data,
            CancellationToken cancellationToken = default
        )
        {
            using var memoryStream = new MemoryStream();

            await using (var gzipStream = new GZipStream(memoryStream, CompressionMode.Compress))
            {
                await
                    gzipStream
                        .WriteAsync(
                            data,
                            cancellationToken
                        );
            }

            return
                memoryStream
                    .ToArray();
        }

        public async Task<byte[]> DecompressAsync(byte[] data, CancellationToken cancellationToken = default)
        {
            using var memoryStream = new MemoryStream(data);

            await using var gzipStream = new GZipStream(memoryStream, CompressionMode.Decompress);

            using var decompressedStream = new MemoryStream();

            await
                gzipStream
                    .CopyToAsync(decompressedStream, cancellationToken);

            return
                decompressedStream
                    .ToArray();
        }
    }
}