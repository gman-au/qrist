using System.Collections.Generic;
using System.Text.Json.Serialization;
using Qrist.UiExtensions.Todoist.Definition.Actions;

namespace Qrist.UiExtensions.Todoist.Definition.CardElements
{
    public class Container : CardElement
    {
        [JsonPropertyName("type")]
        public override string Type => "Container";

        [JsonPropertyName("items")]
        public List<CardElement> Items { get; set; } = [];

        [JsonPropertyName("selectAction")]
        public ActionBase SelectAction { get; set; }

        [JsonPropertyName("backgroundImage")]
        public BackgroundImage BackgroundImage { get; set; }

        [JsonPropertyName("verticalContentAlignment")]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public VerticalContentAlignment? VerticalContentAlignment { get; set; }
    }
}