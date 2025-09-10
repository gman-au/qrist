using System.Collections.Generic;
using System.Text.Json.Serialization;
using Qrist.Domain.Todoist.UiExtensions.Actions;

namespace Qrist.Domain.Todoist.UiExtensions.CardElements
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