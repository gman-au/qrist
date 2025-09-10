using System;
using System.Collections.Generic;
using System.Linq;
using Qrist.Domain.Todoist.API;
using Qrist.Domain.Todoist.UiExtensions;
using Qrist.Domain.Todoist.UiExtensions.CardElements;
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

        protected IEnumerable<CardElement> GetListContainerItems(CreateTodoistTaskApiRequest cachedRequest)
        {
            var listContainerItems =
                new List<CardElement>();

            listContainerItems
                .Add(new TextBlock
                {
                    Color = Color.Accent,
                    Size = FontSize.ExtraLarge,
                    Text = "Your current QR bundle"
                });

            var isEmpty =
                !(cachedRequest?.Tasks ?? []).Any();

            if (isEmpty)
                listContainerItems
                    .Add(new TextBlock
                    {
                        Size = FontSize.Medium,
                        Text = "Empty",
                        IsSubtle = true
                    });
            else
                foreach (var task in cachedRequest?.Tasks ?? [])
                    listContainerItems
                        .Add(new RichTextBlock
                        {
                            Separator = true,
                            Inlines =
                            [
                                // Content
                                new TextRun
                                {
                                    Text = task.Content,
                                    Size = FontSize.Medium,
                                    Weight = FontWeight.Default
                                }
                            ]
                        });

            return listContainerItems;
        }
    }
}