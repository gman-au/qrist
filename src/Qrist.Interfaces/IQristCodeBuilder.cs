using System.Threading;
using System.Threading.Tasks;
using Qrist.Domain;

namespace Qrist.Interfaces
{
    public interface IQristCodeBuilder
    {
        public bool IsApplicable(string provider);

        Task<byte[]> GenerateQrCodeAsync(
            BuildQrCodeRequest request,
            CancellationToken cancellationToken = default
        );
    }
}