using System.Text.Json.Serialization;

namespace Qrist.Domain.Todoist.UiExtensions.CardElements
{
    public class TextBlock : CardElement
    {
        [JsonPropertyName("type")]
        public override string Type => "TextBlock";

        [JsonPropertyName("text")]
        public string Text { get; set; } = string.Empty;

        [JsonPropertyName("size")]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public FontSize? Size { get; set; }

        [JsonPropertyName("isSubtle")]
        public bool? IsSubtle { get; set; }

        [JsonPropertyName("horizontalAlignment")]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public HorizontalAlignment? HorizontalAlignment { get; set; }

        [JsonPropertyName("weight")]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public FontWeight? Weight { get; set; }

        [JsonPropertyName("color")]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public Color? Color { get; set; }

        [JsonPropertyName("wrap")]
        public bool? Wrap { get; set; }
    }
}