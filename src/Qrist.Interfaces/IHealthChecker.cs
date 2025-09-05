using System.Threading;
using System.Threading.Tasks;

namespace Qrist.Interfaces
{
    public interface IHealthChecker
    {
        public Task<string> CheckHealthAsync(CancellationToken cancellationToken = default);
    }
}