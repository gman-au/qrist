using System.Threading;
using System.Threading.Tasks;

namespace Qrist.Interfaces
{
    public interface IRequestDecoder
    {
        Task<byte[]> ProcessAsync(string base64QrCode,
            CancellationToken cancellationToken = default);
    }
}