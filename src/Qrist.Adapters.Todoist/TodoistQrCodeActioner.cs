using System;
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

        public async Task ProcessAsync(
            QrCodeRequest qrCodeRequest,
            CancellationToken cancellationToken = default
        )
        {
            var data =
                JsonSerializer
                    .Deserialize<CreateTodoistTaskRequest>(qrCodeRequest?.Data);

        }
    }
}