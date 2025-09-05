using System.Text.Json.Serialization;

namespace Qrist.Adapters.Todoist.Definition
{
    public class CreateTodoistTaskRequest
    {
        [JsonPropertyName("content")] public string Content { get; set; }

        [JsonPropertyName("description")] public string Description { get; set; }

        [JsonPropertyName("labels")] public string[] Labels { get; set; }

        [JsonPropertyName("priority")] public int Priority { get; set; }
    }
}