using System.Collections.Generic;
using System.Text.Json.Serialization;
using Qrist.Domain.Todoist.UiExtensions.Bridges;

namespace Qrist.Domain.Todoist.UiExtensions.Responses
{
    public class TodoistResponse
    {
        [JsonPropertyName("card")]
        public AdaptiveCard Card { get; set; }

        [JsonPropertyName("bridges")]
        public List<Bridge> Bridges { get; set; }
    }
}