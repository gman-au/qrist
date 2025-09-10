using Qrist.Domain.Todoist;
using Qrist.Domain.Todoist.UiExtensions.Bridges;
using Qrist.Domain.Todoist.UiExtensions.Requests;
using Qrist.Domain.Todoist.UiExtensions.Responses;

namespace Qrist.Adapters.Todoist.UiExtensions.Handlers
{
    public class CancelConfirmationHandler : ActionHandlerBase, ITodoistActionHandler
    {
        public bool IsApplicable(TodoistRequest request) =>
            request?.Action is
            {
                ActionType: TodoistConstants.ActionTypeSubmit,
                ActionId: TodoistConstants.CancelConfirmation
            };

        public TodoistResponse Handle(TodoistRequest request)
        {
            var response =
                new TodoistResponse
                {
                    Bridges =
                    {
                        new DisplayNotificationBridge
                        {
                            Notification = new Notification
                            {
                                Text = "Action cancelled",
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