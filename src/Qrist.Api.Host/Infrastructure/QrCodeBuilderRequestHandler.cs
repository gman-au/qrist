using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Qrist.Domain;
using Qrist.Interfaces;

namespace Qrist.Api.Host.Infrastructure
{
    public class QrCodeBuilderRequestHandler(
        IEnumerable<IQristCodeBuilder> codeBuilders)
        : IQrCodeBuilderRequestHandler
    {
        public async Task HandleAsync(
            BuildQrCodeRequest request,
            CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrEmpty(request.Provider))
                throw new System.ArgumentException("Provider is required");
        }
    }
}