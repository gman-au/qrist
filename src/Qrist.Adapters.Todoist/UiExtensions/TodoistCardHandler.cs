using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Qrist.Adapters.Todoist.UiExtensions.Handlers;
using Qrist.Domain.Todoist.UiExtensions.Requests;
using Qrist.Domain.Todoist.UiExtensions.Responses;

namespace Qrist.Adapters.Todoist.UiExtensions
{
    public class TodoistCardHandler(
        ILogger<TodoistCardHandler> logger,
        IEnumerable<ITodoistActionHandler> handlers)
        : ITodoistCardHandler
    {
        public async Task<TodoistResponse> ProcessAsync(TodoistRequest request)
        {
            try
            {
                var matchingHandler =
                    handlers
                        .FirstOrDefault(o => o.IsApplicable(request));

                if (matchingHandler == null)
                    throw new Exception("No matching TodoistActionHandler found in configuration.");

                return
                    matchingHandler
                        .Handle(request);
            }
            catch (Exception ex)
            {
                logger
                    .LogError("There was an error processing the Todoist UI Extension request: {message}", ex.Message);

                throw;
            }
        }
    }
}