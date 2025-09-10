using System.Text.Json.Serialization;

namespace Qrist.Domain.Todoist.UiExtensions.Contexts
{
    public class Filter
    {
        [JsonPropertyName("id")]
        public string Id { get; set; } = string.Empty;

        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;
    }
}