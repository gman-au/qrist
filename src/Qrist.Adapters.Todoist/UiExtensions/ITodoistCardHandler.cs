using System.Threading.Tasks;
using Qrist.Domain.Todoist.UiExtensions.Requests;
using Qrist.Domain.Todoist.UiExtensions.Responses;

namespace Qrist.Adapters.Todoist.UiExtensions
{
    public interface ITodoistCardHandler
    {
        Task<TodoistResponse> ProcessAsync(TodoistRequest request);
    }
}