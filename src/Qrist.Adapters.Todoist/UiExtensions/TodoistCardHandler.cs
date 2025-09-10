using System;
using System.Threading.Tasks;
using Qrist.Domain.Todoist.UiExtensions;
using Qrist.Domain.Todoist.UiExtensions.Actions;
using Qrist.Domain.Todoist.UiExtensions.Bridges;
using Qrist.Domain.Todoist.UiExtensions.CardElements;
using Qrist.Domain.Todoist.UiExtensions.Requests;
using Qrist.Domain.Todoist.UiExtensions.Responses;

namespace Qrist.Adapters.Todoist.UiExtensions
{
    public class TodoistCardHandler : ITodoistCardHandler
    {
        public async Task<TodoistResponse> ProcessAsync(TodoistRequest request)
        {
            if (request.Action.ActionType == "initial")
            {
                var card = new AdaptiveCard();
                card.Body.Add(new TextBlock { Text = "Hello from C#!" });
                card.Body.Add(new ActionSet
                {
                    Actions = { new ActionSubmit { Id = "submit", Title = "Click me!" } }
                });

                return new TodoistResponse { Card = card };
            }

            return new TodoistResponse { Bridges = { new FinishedBridge() }  };
        }
    }
}