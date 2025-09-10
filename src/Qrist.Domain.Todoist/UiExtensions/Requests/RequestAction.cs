using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Qrist.Domain.Todoist.UiExtensions.Requests
{
    public class RequestAction
    {
        [JsonPropertyName("actionType")]
        public string ActionType { get; set; } = string.Empty;

        [JsonPropertyName("actionId")]
        public string ActionId { get; set; }

        [JsonPropertyName("params")]
        public RequestActionParams Params { get; set; }

        [JsonPropertyName("inputs")]
        public Dictionary<string, JsonElement> Inputs { get; set; }

        [JsonPropertyName("data")]
        public JsonElement? Data { get; set; }
    }
}