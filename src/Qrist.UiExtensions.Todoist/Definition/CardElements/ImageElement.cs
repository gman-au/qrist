using System.Text.Json.Serialization;
using Qrist.UiExtensions.Todoist.Definition.Actions;

namespace Qrist.UiExtensions.Todoist.Definition.CardElements
{
    public class ImageElement : CardElement
    {
        [JsonPropertyName("type")]
        public override string Type => "Image";

        [JsonPropertyName("url")]
        public string Url { get; set; } = string.Empty;

        [JsonPropertyName("selectAction")]
        public ActionBase SelectAction { get; set; }

        [JsonPropertyName("altText")]
        public string AltText { get; set; }

        [JsonPropertyName("height")]
        public string Height { get; set; }

        [JsonPropertyName("width")]
        public string Width { get; set; }

        [JsonPropertyName("size")]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public ImageSize Size { get; set; }
    }
}