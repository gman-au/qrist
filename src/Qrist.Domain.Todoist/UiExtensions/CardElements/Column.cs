using System.Collections.Generic;
using System.Text.Json.Serialization;
using Qrist.Domain.Todoist.UiExtensions.Actions;

namespace Qrist.Domain.Todoist.UiExtensions.CardElements
{
    public class Column : CardElement
    {
        [JsonPropertyName("type")]
        public override string Type => "Column";

        [JsonPropertyName("items")]
        public List<CardElement> Items { get; set; } = [];

        [JsonPropertyName("verticalContentAlignment")]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public VerticalContentAlignment? VerticalContentAlignment { get; set; }

        [JsonPropertyName("width")]
        public string Width { get; set; }

        [JsonPropertyName("selectAction")]
        public ActionBase SelectAction { get; set; }
    }
}