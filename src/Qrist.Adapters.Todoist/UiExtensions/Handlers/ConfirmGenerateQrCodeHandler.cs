using System.Collections.Generic;
using System.Linq;
using Qrist.Domain.Todoist;
using Qrist.Domain.Todoist.UiExtensions;
using Qrist.Domain.Todoist.UiExtensions.Actions;
using Qrist.Domain.Todoist.UiExtensions.CardElements;
using Qrist.Domain.Todoist.UiExtensions.Requests;
using Qrist.Domain.Todoist.UiExtensions.Responses;

namespace Qrist.Adapters.Todoist.UiExtensions.Handlers
{
    public class ConfirmGenerateQrCodeHandler(ITodoistQrBundleCache todoistQrBundleCache)
        : ActionHandlerBase, ITodoistActionHandler
    {
        public bool IsApplicable(TodoistRequest request) =>
            request?.Action is
            {
                ActionType: TodoistConstants.ActionTypeSubmit,
                ActionId: TodoistConstants.ConfirmGenerateQrCode
            };

        public TodoistResponse Handle(TodoistRequest request)
        {
            var id =
                GetId(request);

            var card =
                new AdaptiveCard();

            var cachedRequest =
                todoistQrBundleCache
                    .RetrieveById(id);

            var confirmationItems =
                new List<CardElement>();

            var listContainerItems
                = GetListContainerItems(cachedRequest);

            var actionContainerItems =
                new List<CardElement>();

            var topLevelContainerItems =
                new List<CardElement>();

            confirmationItems
                .Add(new TextBlock
                {
                    Color = Color.Accent,
                    Size = FontSize.Large,
                    Weight = FontWeight.Bolder,
                    Text = "Do you wish to generate a QR code for this bundle?"
                });

            actionContainerItems
                .Add(new ActionSet
                {
                    Spacing = Spacing.Default,
                    Actions =
                    {
                        new ActionSubmit
                        {
                            Id = TodoistConstants.ActionGenerateQrCode,
                            Style = ActionStyle.Destructive,
                            Title = "Confirm"
                        },
                        new ActionSubmit
                        {
                            Id = TodoistConstants.CancelConfirmation,
                            Style = ActionStyle.Default,
                            Title = "Cancel"
                        }
                    }
                });

            topLevelContainerItems
                .Add(
                    new Container
                    {
                        Spacing = Spacing.Large,
                        Separator = true,
                        Items =
                            listContainerItems
                                .ToList()
                    });

            topLevelContainerItems
                .Add(
                    new Container
                    {
                        Spacing = Spacing.Large,
                        Separator = true,
                        Items = confirmationItems
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
                    Items = topLevelContainerItems
                });

            return new TodoistResponse { Card = card };
        }
    }
}