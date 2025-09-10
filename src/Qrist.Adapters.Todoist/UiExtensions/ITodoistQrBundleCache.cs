using Qrist.Domain.Todoist.API;

namespace Qrist.Adapters.Todoist.UiExtensions
{
    public interface ITodoistQrBundleCache
    {
        void Store(CreateTodoistTaskApiRequest task, long id);

        void Clear(long id);

        CreateTodoistTaskApiRequest RetrieveById(long id);
    }
}