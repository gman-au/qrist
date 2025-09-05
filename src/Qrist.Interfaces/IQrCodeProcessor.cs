using System.Threading;
using System.Threading.Tasks;

namespace Qrist.Interfaces
{
    public interface IQrCodeProcessor
    {
        Task ProcessAsync(
            string base64QrCode,
            CancellationToken cancellationToken = default
        );
    }
}