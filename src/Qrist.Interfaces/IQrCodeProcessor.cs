using System;
using System.Threading;
using System.Threading.Tasks;

namespace Qrist.Interfaces
{
    public interface IQrCodeProcessor
    {
        Task<string> GetConfirmationAsync(
            Guid sessionId,
            CancellationToken cancellationToken = default
        );

        Task ProcessActionAsync(
            Guid sessionId,
            CancellationToken cancellationToken = default
        );
    }
}