using System.Text.Json.Serialization;

namespace Qrist.Domain.Todoist.UiExtensions.Actions
{
    public class ActionClipboard : ActionBase
    {
        [JsonPropertyName("type")]
        public override string Type => "Action.Clipboard";

        [JsonPropertyName("text")]
        public string Text { get; set; } = string.Empty;
    }
}