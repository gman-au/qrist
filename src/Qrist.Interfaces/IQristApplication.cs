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

        Task<string> GetQrCodeActionConfirmationAsync(
            string code,
            CancellationToken cancellationToken = default);

        Task ProcessQrCodeActionAsync(
            string code,
            CancellationToken cancellationToken = default
        );
    }
}