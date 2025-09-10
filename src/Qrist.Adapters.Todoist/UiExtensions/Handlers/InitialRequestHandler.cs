using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Options;
using Qrist.Adapters.Todoist.Options;
using Qrist.Domain.Todoist;
using Qrist.Domain.Todoist.UiExtensions;
using Qrist.Domain.Todoist.UiExtensions.Actions;
using Qrist.Domain.Todoist.UiExtensions.CardElements;
using Qrist.Domain.Todoist.UiExtensions.Requests;
using Qrist.Domain.Todoist.UiExtensions.Responses;

namespace Qrist.Adapters.Todoist.UiExtensions.Handlers
{
    public class InitialRequestHandler(
        ITodoistQrBundleCache todoistQrBundleCache,
        IOptions<TodoistConfigurationOptions> optionsAccessor) : ActionHandlerBase, ITodoistActionHandler
    {
        private readonly TodoistConfigurationOptions _options = optionsAccessor.Value;
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

            var listContainerItems =
                new List<CardElement>();

            var nextContainerItems =
                new List<CardElement>();

            var actionContainerItems =
                new List<CardElement>();

            var topLevelContainerItems =
                new List<CardElement>();

            listContainerItems
                .Add(new TextBlock
                {
                    Color = Color.Accent,
                    Size = FontSize.ExtraLarge,
                    Text = "Your current QR bundle"
                });

            if (!(cachedRequest?.Tasks ?? []).Any())
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

            if (alreadyExists)
            {
                nextContainerItems
                    .Add(new TextBlock
                    {
                        Color = Color.Warning,
                        Spacing = Spacing.Large,
                        Size = FontSize.ExtraLarge,
                        Text = "This item is already in your QR bundle.",
                        Separator = true
                    });
            }
            else
            {
                nextContainerItems
                    .Add(new TextBlock
                    {
                        Color = Color.Good,
                        Spacing = Spacing.Default,
                        Size = FontSize.ExtraLarge,
                        Text = "Item to add",
                        Separator = true
                    });

                nextContainerItems
                    .Add(new TextBlock
                    {
                        Spacing = Spacing.Default,
                        Size = FontSize.Medium,
                        Text = $"{action?.Params?.Content}"
                    });
            }

            actionContainerItems
                .Add(new ActionSet
                {
                    Spacing = Spacing.Default,
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

            topLevelContainerItems
                .Add(
                    new Container
                    {
                        Spacing = Spacing.Large,
                        Separator = true,
                        Items = listContainerItems
                    });

            topLevelContainerItems
                .Add(
                    new Container
                    {
                        Spacing = Spacing.Large,
                        Separator = true,
                        Items = nextContainerItems
                    });

            topLevelContainerItems
                .Add(
                    new Container
                    {
                        Spacing = Spacing.Large,
                        Separator = true,
                        Items = actionContainerItems
                    });

            card
                .Body
                .Add(new Container
                {
                    Spacing = Spacing.Large,
                    Separator = true,
                    Items = topLevelContainerItems,
                    BackgroundImage = new BackgroundImage
                    {
                        Url = _options?.BackgroundImageUrl,
                        FillMode = ImageFillMode.Cover
                    }
                });

            return new TodoistResponse { Card = card };
        }
    }
}