using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Qrist.UiExtensions.Todoist.Definition.CardElements
{
    public class ColumnSet : CardElement
    {
        [JsonPropertyName("type")]
        public override string Type => "ColumnSet";

        [JsonPropertyName("columns")]
        public List<Column> Columns { get; set; } = [];

        [JsonPropertyName("horizontalAlignment")]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public HorizontalAlignment? HorizontalAlignment { get; set; }
    }
}