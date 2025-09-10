using System.Collections.Generic;
using System.Text.Json.Serialization;
using Qrist.Domain.Todoist.UiExtensions.Actions;
using Qrist.Domain.Todoist.UiExtensions.CardElements;

namespace Qrist.Domain.Todoist.UiExtensions
{
    public class AdaptiveCard
    {
        [JsonPropertyName("type")]
        public string Type { get; set; } = "AdaptiveCard";

        [JsonPropertyName("$schema")]
        public string Schema { get; set; } = "http://adaptivecards.io/schemas/adaptive-card.json";

        [JsonPropertyName("doistCardVersion")]
        public string DoistCardVersion { get; set; } = "0.6";

        [JsonPropertyName("version")]
        public string Version { get; set; } = "1.4";

        [JsonPropertyName("body")]
        public List<CardElement> Body { get; set; } = [];

        [JsonPropertyName("actions")]
        public List<ActionBase> Actions { get; set; } = [];
    }
}