using System.Threading;
using System.Threading.Tasks;
using Qrist.Domain;

namespace Qrist.Interfaces
{
    public interface IRequestActioner
    {
        bool IsApplicable(string provider);

        Task<string> GetConfirmationAsync(
            QrCodeRequest qrCodeRequest,
            CancellationToken cancellationToken = default
        );

        Task ProcessAsync(
            QrCodeRequest requestData,
            CancellationToken cancellationToken = default
        );
    }
}