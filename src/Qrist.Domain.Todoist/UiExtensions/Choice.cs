using System.Text.Json.Serialization;

namespace Qrist.Domain.Todoist.UiExtensions
{
    public class Choice
    {
        [JsonPropertyName("title")]
        public string Title { get; set; } = string.Empty;

        [JsonPropertyName("value")]
        public string Value { get; set; }

        [JsonPropertyName("disabled")]
        public bool? Disabled { get; set; }
    }
}