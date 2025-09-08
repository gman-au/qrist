using System.Threading;
using System.Threading.Tasks;

namespace Qrist.Interfaces
{
    public interface IQrCodeProcessor
    {
        Task<string> GetConfirmationAsync(
            string base64QrCode,
            CancellationToken cancellationToken = default
        );

        Task ProcessActionAsync(
            string base64QrCode,
            CancellationToken cancellationToken = default
        );
    }
}