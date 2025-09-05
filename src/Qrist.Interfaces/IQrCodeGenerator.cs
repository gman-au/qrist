using System.Threading;
using System.Threading.Tasks;

namespace Qrist.Interfaces
{
    public interface IQrCodeGenerator
    {
        Task<string> GenerateAsync(
            string value,
            CancellationToken cancellationToken = default
        );
    }
}