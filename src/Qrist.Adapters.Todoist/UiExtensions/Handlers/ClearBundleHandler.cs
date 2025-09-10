using Qrist.Domain.Todoist;
using Qrist.Domain.Todoist.UiExtensions.Bridges;
using Qrist.Domain.Todoist.UiExtensions.Requests;
using Qrist.Domain.Todoist.UiExtensions.Responses;

namespace Qrist.Adapters.Todoist.UiExtensions.Handlers
{
    public class ClearBundleHandler(ITodoistQrBundleCache todoistQrBundleCache) : ActionHandlerBase, ITodoistActionHandler
    {
        public bool IsApplicable(TodoistRequest request) =>
            request?.Action is
            {
                ActionType: TodoistConstants.ActionTypeSubmit,
                ActionId: TodoistConstants.ActionClearBundle
            };

        public TodoistResponse Handle(TodoistRequest request)
        {
            var id =
                GetId(request);

            todoistQrBundleCache
                .Clear(id);

            var response =
                new TodoistResponse
                {
                    Bridges =
                    {
                        new DisplayNotificationBridge
                        {
                            Notification = new Notification
                            {
                                Text = "Your QR bundle has been cleared",
                                Type = "success"
                            }
                        },
                        new FinishedBridge()
                    }
                };

            return response;
        }
    }
}