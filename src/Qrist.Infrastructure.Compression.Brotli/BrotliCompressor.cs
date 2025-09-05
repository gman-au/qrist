using System.IO;
using System.IO.Compression;
using System.Threading;
using System.Threading.Tasks;
using Qrist.Interfaces;

namespace Qrist.Infrastructure.Compression.Brotli
{
    public class BrotliCompressor : ICompressor
    {
        public async Task<byte[]> CompressAsync(byte[] data, CancellationToken cancellationToken = default)
        {
            using var memoryStream = new MemoryStream();

            await using (var brotliStream = new BrotliStream(memoryStream, CompressionLevel.Optimal))
            {
                await
                    brotliStream
                        .WriteAsync(data, cancellationToken);
            }

            return
                memoryStream
                    .ToArray();
        }

        public async Task<byte[]> DecompressAsync(byte[] data, CancellationToken cancellationToken = default)
        {
            using var memoryStream = new MemoryStream(data);

            await using var brotliStream = new BrotliStream(memoryStream, CompressionMode.Decompress);

            using var decompressedStream = new MemoryStream();

            await
                brotliStream
                    .CopyToAsync(decompressedStream, cancellationToken);

            return
                decompressedStream
                    .ToArray();
        }
    }
}