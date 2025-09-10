using System.Text.Json.Serialization;

namespace Qrist.UiExtensions.Todoist.Definition.Requests
{
    public class RequestActionParams
    {
        [JsonPropertyName("source")]
        public string Source { get; set; } = string.Empty;

        [JsonPropertyName("sourceId")]
        public string SourceId { get; set; } = string.Empty;

        [JsonPropertyName("url")]
        public string Url { get; set; } = string.Empty;

        [JsonPropertyName("content")]
        public string Content { get; set; } = string.Empty;

        [JsonPropertyName("contentPlain")]
        public string ContentPlain { get; set; } = string.Empty;
    }
}