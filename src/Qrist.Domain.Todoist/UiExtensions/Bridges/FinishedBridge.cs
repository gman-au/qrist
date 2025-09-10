using System.Text.Json.Serialization;

namespace Qrist.Domain.Todoist.UiExtensions.Bridges
{
    public class FinishedBridge : Bridge
    {
        [JsonPropertyName("bridgeActionType")]
        public override string BridgeActionType => "finished";
    }
}