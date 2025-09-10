using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Qrist.Infrastructure.Web
{
    public interface ITodoistRequestValidator
    {
        Task<bool> IsValidAsync(HttpRequest httpRequest);
    }
}