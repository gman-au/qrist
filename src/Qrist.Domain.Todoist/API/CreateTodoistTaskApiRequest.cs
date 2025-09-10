using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Qrist.Domain.Todoist.API
{
    public class CreateTodoistTaskApiRequest
    {
        [JsonPropertyName("tasks")] public IEnumerable<TodoistApiTask> Tasks { get; set; }
    }
}