using System.Linq;
using Qrist.Domain.Todoist;
using Qrist.Domain.Todoist.UiExtensions;
using Qrist.Domain.Todoist.UiExtensions.Actions;
using Qrist.Domain.Todoist.UiExtensions.CardElements;
using Qrist.Domain.Todoist.UiExtensions.Requests;
using Qrist.Domain.Todoist.UiExtensions.Responses;

namespace Qrist.Adapters.Todoist.UiExtensions.Handlers
{
    public class InitialRequestHandler(ITodoistQrBundleCache todoistQrBundleCache) : ActionHandlerBase, ITodoistActionHandler
    {
        public bool IsApplicable(TodoistRequest request) => request?.Action?.ActionType == TodoistConstants.ActionTypeInitial;

        public TodoistResponse Handle(TodoistRequest request)
        {
            var id =
                GetId(request);

            var action =
                request?
                    .Action;

            var cachedRequest =
                todoistQrBundleCache
                    .RetrieveById(id);

            var sourceId =
                GetSourceId(request);

            var alreadyExists =
                (cachedRequest?
                    .Tasks ?? [])
                .Any(o => o.SourceId == sourceId);

            var card =
                new AdaptiveCard();

            card
                .Body
                .Add(new TextBlock
                {
                    Color = Color.Attention,
                    Size = FontSize.ExtraLarge,
                    Text = "Your current QR bundle"
                });

            card
                .Body
                .Add(new RichTextBlock
                {
                    Inlines =
                    [
                        new TextRun
                        {
                            Text = "testing",
                            Color = Color.Accent,
                            Size = null,
                            IsSubtle = null,
                            Weight = FontWeight.Default
                        }
                    ]
                });

            if (!cachedRequest.Tasks.Any())
                card
                    .Body
                    .Add(new TextBlock
                    {
                        Size = FontSize.Medium,
                        Text = "Empty",
                        IsSubtle = true
                    });
            else
                foreach (var task in cachedRequest.Tasks)
                    card
                        .Body
                        .Add(new TextBlock
                        {
                            Spacing = Spacing.Large,
                            Size = FontSize.Medium,
                            Text = $"• {task.Content}"
                        });

            if (alreadyExists)
            {
                card
                    .Body
                    .Add(new TextBlock
                    {
                        Color = Color.Warning,
                        Spacing = Spacing.Large,
                        Size = FontSize.ExtraLarge,
                        Text = "This item is already in your QR bundle",
                        Separator = true
                    });
            }
            else
            {
                card
                    .Body
                    .Add(new TextBlock
                    {
                        Color = Color.Attention,
                        Spacing = Spacing.Large,
                        Size = FontSize.ExtraLarge,
                        Text = "Item to add",
                        Separator = true
                    });

                card
                    .Body
                    .Add(new TextBlock
                    {
                        Spacing = Spacing.Large,
                        Size = FontSize.Medium,
                        Text = $"{action?.Params?.Content}"
                    });
            }

            card
                .Body
                .Add(new ActionSet
                {
                    Spacing = Spacing.Large,
                    Actions =
                    {
                        alreadyExists
                            ? null
                            : new ActionSubmit
                            {
                                Id = TodoistConstants.ActionAddToBundle,
                                Style = ActionStyle.Positive,
                                Title = "Add this item to my QR bundle"
                            },
                        new ActionSubmit
                        {
                            Id = TodoistConstants.ActionClearBundle,
                            Style = ActionStyle.Destructive,
                            Title = "Clear my QR bundle"
                        }
                    }
                });

            return new TodoistResponse { Card = card };
        }
    }
}