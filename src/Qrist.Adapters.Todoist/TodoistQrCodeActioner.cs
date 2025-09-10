using System;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Qrist.Adapters.Todoist.Options;
using Qrist.Domain;
using Qrist.Domain.Todoist.API;
using Qrist.Interfaces;

namespace Qrist.Adapters.Todoist
{
    public class TodoistQrCodeActioner(
        IOptions<TodoistConfigurationOptions> optionsAccessor,
        ILogger<TodoistQrCodeActioner> logger) : IRequestActioner
    {
        private readonly TodoistConfigurationOptions _options = optionsAccessor.Value;

        public bool IsApplicable(string provider) =>
            string
                .Equals(
                    provider,
                    TodoistConstants.Provider,
                    StringComparison.InvariantCultureIgnoreCase
                );

        public async Task<string> GetConfirmationAsync(
            QrCodeRequest qrCodeRequest,
            CancellationToken cancellationToken = default)
        {
            var taskRequest =
                JsonSerializer
                    .Deserialize<CreateTodoistTaskApiRequest>(qrCodeRequest?.Data) as CreateTodoistTaskApiRequest;

            var confirmationMessage = new StringBuilder();

            confirmationMessage
                .AppendLine($"#### Confirm the following {TodoistConstants.Provider} item(s) to add:");

            foreach (var task in taskRequest?.Tasks ?? [])
            {
                confirmationMessage
                    .AppendLine($"- **{task.Content}**");

                if (!string.IsNullOrEmpty(task.Description))
                {
                    confirmationMessage
                        .AppendLine($"\t - {task.Description}");
                }

                if ((task.Labels ?? []).Any())
                {
                    confirmationMessage
                        .AppendLine($"\t - _Labels: {string.Join(", ", task.Labels)}_");
                }
            }

            return
                confirmationMessage
                    .ToString();
        }

        public async Task ProcessAsync(
            QrCodeRequest qrCodeRequest,
            SessionStateItem sessionStateItem,
            CancellationToken cancellationToken = default
        )
        {
            var taskRequest =
                JsonSerializer
                    .Deserialize<CreateTodoistTaskApiRequest>(qrCodeRequest?.Data) as CreateTodoistTaskApiRequest;

            var tasks =
                (taskRequest?.Tasks ?? [])
                .ToList();

            logger
                .LogInformation("Adding {count} tasks for session ID {sessionId}", tasks.Count, sessionStateItem.Id);

            var client = new HttpClient();

            client.BaseAddress =
                new Uri(
                    _options?
                        .CreateTaskEndpoint ??
                    throw new Exception($"{nameof(_options.CreateTaskEndpoint)} not configured")
                );

            foreach (var task in tasks)
                try
                {
                    var request =
                        new HttpRequestMessage(
                            HttpMethod.Post,
                            ""
                        );

                    request
                        .Headers
                        .Add(
                            "ContentType",
                            "application/json"
                        );

                    request
                        .Headers
                        .Add(
                            "Authorization",
                            $"Bearer {sessionStateItem.AccessToken}"
                        );

                    var jsonTask =
                        JsonSerializer
                            .Serialize(task);

                    request.Content =
                        new StringContent(
                            jsonTask,
                            Encoding.Default,
                            "application/json"
                        );

                    var httpResponse =
                        await
                            client
                                .SendAsync(request, cancellationToken);

                    httpResponse
                        .EnsureSuccessStatusCode();

                    logger
                        .LogInformation("Success - added task {content} for session ID {sessionId}", task.Content, sessionStateItem.Id);
                }
                catch (Exception ex)
                {
                    logger
                        .LogError("Error adding task {content} for session ID {sessionId}: {message}",
                            task.Content,
                            sessionStateItem.Id,
                            ex.Message);
                }
        }
    }
}