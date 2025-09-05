using System;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Qrist.Adapters.Todoist.Definition;
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
            dynamic requestData,
            CancellationToken cancellationToken = default
        )
        {
            var request =
                JsonSerializer
                    .Deserialize<CreateTodoistTaskRequest>(requestData);
        }
    }
}