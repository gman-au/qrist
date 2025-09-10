using System.Text.Json.Serialization;

namespace Qrist.UiExtensions.Todoist.Definition.Bridges
{
    public class FinishedBridge : Bridge
    {
        [JsonPropertyName("bridgeActionType")]
        public override string BridgeActionType => "finished";
    }
}