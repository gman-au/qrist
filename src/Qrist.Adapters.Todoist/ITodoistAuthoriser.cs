using System.Threading;
using System.Threading.Tasks;

namespace Qrist.Adapters.Todoist
{
    public interface ITodoistAuthoriser
    {
        Task<string> GetRedirectUrlAsync(CancellationToken cancellationToken = default);

        Task<string> GetAccessTokenAsync(string code, CancellationToken cancellationToken = default);
    }
}