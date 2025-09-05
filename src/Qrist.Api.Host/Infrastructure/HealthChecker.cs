using System.Threading;
using System.Threading.Tasks;
using Qrist.Interfaces;

namespace Qrist.Api.Host.Infrastructure
{
    public class HealthChecker : IHealthChecker
    {
        public async Task<string> CheckHealthAsync(CancellationToken cancellationToken = default)
        {
            return "OK";
        }
    }
}