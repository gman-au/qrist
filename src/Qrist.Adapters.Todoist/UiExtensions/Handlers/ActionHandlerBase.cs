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
                throw new Exception("ID not found in Todoist request");

            return id.Value;
        }

        protected string GetSourceId(TodoistRequest request)
        {
            var sourceId =
                request?
                    .Action?
                    .Params?
                    .SourceId;

            if (string.IsNullOrEmpty(sourceId))
                throw new Exception("Source ID not found in Todoist request");

            return sourceId;
        }
    }
}