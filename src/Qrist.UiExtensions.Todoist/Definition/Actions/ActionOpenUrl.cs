using System.Text.Json.Serialization;

namespace Qrist.UiExtensions.Todoist.Definition.Actions
{
    public class ActionOpenUrl : ActionBase
    {
        [JsonPropertyName("type")]
        public override string Type => "Action.OpenUrl";

        [JsonPropertyName("url")]
        public string Url { get; set; } = string.Empty;
    }
}