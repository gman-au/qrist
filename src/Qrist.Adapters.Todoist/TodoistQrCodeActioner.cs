using System;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Qrist.Adapters.Todoist.Definition;
using Qrist.Domain;
using Qrist.Interfaces;

namespace Qrist.Adapters.Todoist
{
    public class TodoistQrCodeActioner : IRequestActioner
    {
        private const string TodoistProvider = "TODOIST";

        public bool IsApplicable(string provider) =>
            string
                .Equals(
                    provider,
                    TodoistProvider,
                    StringComparison.InvariantCultureIgnoreCase
                );

        public async Task<string> GetConfirmationAsync(
            QrCodeRequest qrCodeRequest,
            CancellationToken cancellationToken = default)
        {
            var taskRequest =
                JsonSerializer
                    .Deserialize<CreateTodoistTaskRequest>(qrCodeRequest?.Data) as CreateTodoistTaskRequest;

            var confirmationMessage = new StringBuilder();

            confirmationMessage
                .AppendLine($"Confirm the following {TodoistProvider} item(s) to add:");

            foreach (var task in taskRequest?.Tasks ?? [])
            {
                confirmationMessage
                    .AppendLine($"- {task.Content}")
                    .AppendLine(string.IsNullOrEmpty(task.Description) ? null : $"\t{task.Description}");
            }

            return
                confirmationMessage
                    .ToString();
        }

        public async Task ProcessAsync(
            QrCodeRequest qrCodeRequest,
            CancellationToken cancellationToken = default
        )
        {
            var data =
                JsonSerializer
                    .Deserialize<CreateTodoistTaskRequest>(qrCodeRequest?.Data) as CreateTodoistTaskRequest;
        }
    }
}