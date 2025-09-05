using System.Threading;
using System.Threading.Tasks;
using Qrist.Domain;

namespace Qrist.Interfaces
{
    public interface IQrCodeEncoder
    {
        Task<byte[]> ProcessAsync(
            QrCodeRequest request,
            CancellationToken cancellationToken = default
        );
    }
}