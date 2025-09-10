using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Qrist.UiExtensions.Todoist.Definition.CardElements
{
    public class RichTextBlock : CardElement
    {
        [JsonPropertyName("type")]
        public override string Type => "RichTextBlock";

        [JsonPropertyName("inlines")]
        public List<Inline> Inlines { get; set; } = [];

        [JsonPropertyName("horizontalAlignment")]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public HorizontalAlignment? HorizontalAlignment { get; set; }
    }
}