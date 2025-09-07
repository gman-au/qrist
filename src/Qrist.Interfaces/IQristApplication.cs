using System.Threading;
using System.Threading.Tasks;
using Qrist.Domain;

namespace Qrist.Interfaces
{
    public interface IQristApplication
    {
        Task<string> ProduceQrCodeAsync(
            QrCodeRequest request,
            CancellationToken cancellationToken = default
        );

        Task ProcessQrCodeAsync(
            string code,
            CancellationToken cancellationToken = default
        );
    }
}