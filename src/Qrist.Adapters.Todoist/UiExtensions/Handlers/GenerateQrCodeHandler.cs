using System.Collections.Generic;
using Microsoft.Extensions.Options;
using Qrist.Adapters.Todoist.Options;
using Qrist.Domain.Todoist;
using Qrist.Domain.Todoist.UiExtensions;
using Qrist.Domain.Todoist.UiExtensions.Actions;
using Qrist.Domain.Todoist.UiExtensions.CardElements;
using Qrist.Domain.Todoist.UiExtensions.Requests;
using Qrist.Domain.Todoist.UiExtensions.Responses;
using Qrist.Interfaces;

namespace Qrist.Adapters.Todoist.UiExtensions.Handlers
{
    public class GenerateQrCodeHandler(
        ITodoistQrBundleCache todoistQrBundleCache,
        IQristApplication qristApplication,
        IOptions<TodoistConfigurationOptions> optionsAccessor) : ActionHandlerBase, ITodoistActionHandler
    {
        private readonly TodoistConfigurationOptions _options = optionsAccessor.Value;

        public bool IsApplicable(TodoistRequest request) =>
            request?.Action is
            {
                ActionType: TodoistConstants.ActionTypeSubmit,
                ActionId: TodoistConstants.ActionGenerateQrCode
            };

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

            // convert CreateTodoistTaskApiRequest to QrCodeRequest
            /*qristApplication
                .ProduceQrCodeAsync();*/

            var card =
                new AdaptiveCard();

            var nextContainerItems =
                new List<CardElement>();

            var actionContainerItems =
                new List<CardElement>();

            var topLevelContainerItems =
                new List<CardElement>();

            // add image as
            nextContainerItems
                .Add(new TextBlock
                {
                    HorizontalAlignment = HorizontalAlignment.Center,
                    Color = Color.Default,
                    Spacing = Spacing.Large,
                    Size = FontSize.Medium,
                    Text = "Your QR code is ready! Download this QR code or print it out for reusable tasks.",
                    Separator = false,
                    Wrap = true
                });

            nextContainerItems
                .Add(new ColumnSet
                {
                    Spacing = Spacing.Large,
                    Separator = true,
                    Columns =
                    [
                        new Column
                        {
                            Width = "auto",
                            Items =
                            [
                                new ImageElement
                                {
                                    Url = TodoistConstants.TransparentBase64SinglePixel,
                                    Width = "50px",
                                    Size = ImageSize.Stretch
                                }
                            ]
                        },
                        new Column
                        {
                            Width = "stretch",
                            Items =
                            [
                                new ImageElement
                                {
                                    Spacing = Spacing.Large,
                                    Url =
                                        "https://www.shutterstock.com/image-photo/domestic-pig-standing-front-sticking-600nw-2472113621.jpg",
                                    AltText = "QR code",
                                    Height = "300px",
                                    Width = "300px",
                                    Size = ImageSize.Auto
                                }
                            ]
                        },
                        new Column
                        {
                            Width = "auto",
                            Items =
                            [
                                new ImageElement
                                {
                                    Url = TodoistConstants.TransparentBase64SinglePixel,
                                    Width = "50px",
                                    Size = ImageSize.Stretch
                                }
                            ]
                        }
                    ],
                    HorizontalAlignment = null
                });

            actionContainerItems
                .Add(new TextBlock
                {
                    HorizontalAlignment = HorizontalAlignment.Center,
                    Color = Color.Default,
                    Spacing = Spacing.Large,
                    Size = FontSize.Medium,
                    Text = "You may close this window when finished.",
                    Separator = false,
                    Wrap = true
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