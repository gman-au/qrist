using System.Threading;
using System.Threading.Tasks;
using Qrist.Domain;

namespace Qrist.Interfaces
{
    public interface IRequestEncoder
    {
        Task<byte[]> ProcessAsync(
            QrCodeRequest request,
            CancellationToken cancellationToken = default
        );
    }
}