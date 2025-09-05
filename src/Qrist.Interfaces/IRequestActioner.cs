using System.Threading;
using System.Threading.Tasks;

namespace Qrist.Interfaces
{
    public interface IRequestActioner
    {
        bool IsApplicable(string provider);

        Task ProcessAsync(
            dynamic requestData,
            CancellationToken cancellationToken = default
        );
    }
}