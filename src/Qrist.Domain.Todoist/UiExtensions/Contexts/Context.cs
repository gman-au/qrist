using System.Text.Json.Serialization;

namespace Qrist.Domain.Todoist.UiExtensions.Contexts
{
    public class Context
    {
        [JsonPropertyName("theme")]
        public string Theme { get; set; } = string.Empty;

        [JsonPropertyName("user")]
        public User User { get; set; } = new();

        [JsonPropertyName("todoist")]
        public TodoistContext Todoist { get; set; } = new();

        [JsonPropertyName("platform")]
        public string Platform { get; set; } = string.Empty;
    }
}