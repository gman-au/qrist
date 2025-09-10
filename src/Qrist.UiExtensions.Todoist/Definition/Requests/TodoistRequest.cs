using System.Text.Json.Serialization;
using Qrist.UiExtensions.Todoist.Definition.Contexts;

namespace Qrist.UiExtensions.Todoist.Definition.Requests
{
    public class TodoistRequest
    {
        [JsonPropertyName("extensionType")]
        public string ExtensionType { get; set; } = string.Empty;

        [JsonPropertyName("context")]
        public Context Context { get; set; } = new();

        [JsonPropertyName("action")]
        public RequestAction Action { get; set; } = new();

        [JsonPropertyName("maximumDoistCardVersion")]
        public decimal MaximumDoistCardVersion { get; set; }
    }
}