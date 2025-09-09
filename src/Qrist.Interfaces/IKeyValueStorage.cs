using System.Threading;
using System.Threading.Tasks;

namespace Qrist.Interfaces
{
    public interface IKeyValueStorage
    {
        Task<string> LookupCodeDataAsync(
            string partitionKey,
            string rowKey,
            CancellationToken cancellationToken
        );

        Task<string> StoreCodeDataAsync(
            string partitionKey,
            string valueData,
            CancellationToken cancellationToken
        );
    }
}