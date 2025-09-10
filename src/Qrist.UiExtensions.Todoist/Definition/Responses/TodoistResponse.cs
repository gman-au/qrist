using System.Collections.Generic;
using System.Text.Json.Serialization;
using Qrist.UiExtensions.Todoist.Definition.Bridges;

namespace Qrist.UiExtensions.Todoist.Definition.Responses
{
    public class TodoistResponse
    {
        [JsonPropertyName("card")]
        public AdaptiveCard Card { get; set; }

        [JsonPropertyName("bridges")]
        public List<Bridge> Bridges { get; set; }
    }
}