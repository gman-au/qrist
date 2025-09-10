using System.Text.Json.Serialization;

namespace Qrist.Domain.Todoist.UiExtensions.Bridges
{
    public class Notification
    {
        [JsonPropertyName("text")]
        public string Text { get; set; } = string.Empty;

        [JsonPropertyName("type")]
        public string Type { get; set; } = string.Empty; // "info", "success", "error"

        [JsonPropertyName("action")]
        public string Action { get; set; }

        [JsonPropertyName("actionText")]
        public string ActionText { get; set; }
    }
}