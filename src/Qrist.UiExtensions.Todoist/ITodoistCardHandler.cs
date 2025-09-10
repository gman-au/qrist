using System.Threading.Tasks;
using Qrist.UiExtensions.Todoist.Definition.Requests;
using Qrist.UiExtensions.Todoist.Definition.Responses;

namespace Qrist.UiExtensions.Todoist
{
    public interface ITodoistCardHandler
    {
        Task<TodoistResponse> ProcessAsync(TodoistRequest request);
    }
}