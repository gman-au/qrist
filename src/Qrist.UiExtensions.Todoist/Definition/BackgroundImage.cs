using System.Text.Json.Serialization;

namespace Qrist.UiExtensions.Todoist.Definition
{
    public class BackgroundImage
    {
        [JsonPropertyName("url")]
        public string Url { get; set; } = string.Empty;

        [JsonPropertyName("fillMode")]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public ImageFillMode? FillMode { get; set; }
    }
}