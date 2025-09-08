using System;
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

        public Task<string> GetQrCodeActionConfirmationAsync(
            Guid sessionId,
            CancellationToken cancellationToken = default);

        Task ProcessQrCodeActionAsync(
            Guid sessionId,
            CancellationToken cancellationToken = default
        );
    }
}