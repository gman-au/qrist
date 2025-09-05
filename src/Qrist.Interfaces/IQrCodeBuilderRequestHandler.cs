using System.Threading;
using System.Threading.Tasks;
using Qrist.Domain;

namespace Qrist.Interfaces
{
    public interface IQrCodeBuilderRequestHandler
    {
        Task<byte[]> HandleAsync(
            BuildQrCodeRequest request,
            CancellationToken cancellationToken = default
        );
    }
}