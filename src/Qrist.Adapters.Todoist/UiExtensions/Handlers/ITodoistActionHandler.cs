using Qrist.Domain.Todoist.UiExtensions.Requests;
using Qrist.Domain.Todoist.UiExtensions.Responses;

namespace Qrist.Adapters.Todoist.UiExtensions.Handlers
{
    public interface ITodoistActionHandler
    {
        bool IsApplicable(TodoistRequest request);

        TodoistResponse Handle(TodoistRequest request);
    }
}