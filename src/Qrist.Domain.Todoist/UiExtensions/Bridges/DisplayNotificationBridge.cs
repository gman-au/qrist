using System.Text.Json.Serialization;

namespace Qrist.Domain.Todoist.UiExtensions.Bridges
{
    public class DisplayNotificationBridge : Bridge
    {
        [JsonPropertyName("bridgeActionType")]
        public override string BridgeActionType => "display.notification";

        [JsonPropertyName("notification")]
        public Notification Notification { get; set; } = new();
    }
}