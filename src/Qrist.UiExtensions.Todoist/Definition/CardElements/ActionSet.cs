using System.Collections.Generic;
using System.Text.Json.Serialization;
using Qrist.UiExtensions.Todoist.Definition.Actions;

namespace Qrist.UiExtensions.Todoist.Definition.CardElements
{
    public class ActionSet : CardElement
    {
        [JsonPropertyName("type")]
        public override string Type => "ActionSet";

        [JsonPropertyName("actions")]
        public List<ActionBase> Actions { get; set; } = [];

        [JsonPropertyName("horizontalAlignment")]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public HorizontalAlignment? HorizontalAlignment { get; set; }
    }
}