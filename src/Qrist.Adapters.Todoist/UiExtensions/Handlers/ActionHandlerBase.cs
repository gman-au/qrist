using System;
using Qrist.Domain.Todoist.UiExtensions.Requests;

namespace Qrist.Adapters.Todoist.UiExtensions.Handlers
{
    public class ActionHandlerBase
    {
        protected long GetId(TodoistRequest request)
        {
            var id =
                request?
                    .Context?
                    .User?
                    .Id;

            if (!id.HasValue)
                throw new Exception("Id not found in Todoist request");

            return id.Value;
        }
    }
}