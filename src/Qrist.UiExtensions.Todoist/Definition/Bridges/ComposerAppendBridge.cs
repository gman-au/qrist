using System.Text.Json.Serialization;

namespace Qrist.UiExtensions.Todoist.Definition.Bridges
{
    public class ComposerAppendBridge : Bridge
    {
        [JsonPropertyName("bridgeActionType")]
        public override string BridgeActionType => "composer.append";

        [JsonPropertyName("text")]
        public string Text { get; set; } = string.Empty;
    }
}