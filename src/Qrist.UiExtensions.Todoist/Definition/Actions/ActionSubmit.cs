using System.Text.Json;
using System.Text.Json.Serialization;

namespace Qrist.UiExtensions.Todoist.Definition.Actions
{
    public class ActionSubmit : ActionBase
    {
        [JsonPropertyName("type")]
        public override string Type => "Action.Submit";

        [JsonPropertyName("data")]
        public JsonElement? Data { get; set; }

        [JsonPropertyName("associatedInputs")]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public AssociatedInputs? AssociatedInputs { get; set; }
    }
}