using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Qrist.Adapters.Todoist.Definition
{
    public class CreateTodoistTaskRequest
    {
        [JsonPropertyName("tasks")] public IEnumerable<TodoistTask> Tasks { get; set; }
    }
}