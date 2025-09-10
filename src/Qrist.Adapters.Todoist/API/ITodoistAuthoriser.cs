using System;
using System.Threading;
using System.Threading.Tasks;

namespace Qrist.Adapters.Todoist.Authorisation
{
    public interface ITodoistAuthoriser
    {
        Task<string> GetRedirectUrlAsync(
            string qrCodeData,
            CancellationToken cancellationToken = default
        );

        Task<Guid?> RetrieveAndCacheAccessTokenByIdAsync(
            string code,
            string state,
            CancellationToken cancellationToken = default
        );
    }
}