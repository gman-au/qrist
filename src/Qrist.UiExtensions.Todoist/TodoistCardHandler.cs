using System;
using System.Threading.Tasks;
using Qrist.UiExtensions.Todoist.Definition;
using Qrist.UiExtensions.Todoist.Definition.Actions;
using Qrist.UiExtensions.Todoist.Definition.Bridges;
using Qrist.UiExtensions.Todoist.Definition.CardElements;
using Qrist.UiExtensions.Todoist.Definition.Requests;
using Qrist.UiExtensions.Todoist.Definition.Responses;

namespace Qrist.UiExtensions.Todoist
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