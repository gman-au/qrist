using System.Linq;
using Qrist.Domain.Todoist;
using Qrist.Domain.Todoist.API;
using Qrist.Domain.Todoist.UiExtensions.Bridges;
using Qrist.Domain.Todoist.UiExtensions.Requests;
using Qrist.Domain.Todoist.UiExtensions.Responses;

namespace Qrist.Adapters.Todoist.UiExtensions.Handlers
{
    public class AddToBundleHandler(ITodoistQrBundleCache todoistQrBundleCache) : ActionHandlerBase, ITodoistActionHandler
    {
        public bool IsApplicable(TodoistRequest request) =>
            request?.Action is
            {
                ActionType: TodoistConstants.ActionTypeSubmit,
                ActionId: TodoistConstants.ActionAddToBundle
            };

        public TodoistResponse Handle(TodoistRequest request)
        {
            var id =
                GetId(request);

            var sourceId =
                GetSourceId(request);

            var action =
                request?
                    .Action;

            var cachedRequest =
                todoistQrBundleCache
                    .RetrieveById(id);

            var taskList =
                cachedRequest
                    .Tasks
                    .ToList();

            taskList
                .Add(new TodoistApiTask
                {
                    SourceId = sourceId,
                    Content = action?.Params?.Content,
                    Description = null,
                    Priority = 0
                });

            cachedRequest
                .Tasks = taskList;

            todoistQrBundleCache
                .Store(cachedRequest, id);

            var response =
                new TodoistResponse
                {
                    Bridges =
                    {
                        new DisplayNotificationBridge
                        {
                            Notification = new Notification
                            {
                                Text = "The item has been added to your QR bundle",
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