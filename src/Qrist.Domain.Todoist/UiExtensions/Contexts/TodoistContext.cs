using System.Text.Json.Serialization;

namespace Qrist.Domain.Todoist.UiExtensions.Contexts
{
    public class TodoistContext
    {
        [JsonPropertyName("project")]
        public Project Project { get; set; }

        [JsonPropertyName("filter")]
        public Filter Filter { get; set; }

        [JsonPropertyName("label")]
        public Label Label { get; set; }

        [JsonPropertyName("additionalUserContext")]
        public AdditionalUserContext AdditionalUserContext { get; set; } = new();
    }
}