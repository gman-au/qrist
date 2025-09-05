using System.Threading;
using System.Threading.Tasks;

namespace Qrist.Interfaces
{
    public interface ICompressor
    {
        public Task<byte[]> CompressAsync(
            byte[] data,
            CancellationToken cancellationToken = default
        );

        public Task<byte[]> DecompressAsync(
            byte[] data,
            CancellationToken cancellationToken = default
        );
    }
}