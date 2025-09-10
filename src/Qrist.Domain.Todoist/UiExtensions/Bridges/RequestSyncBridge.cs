using System.Text.Json.Serialization;

namespace Qrist.Domain.Todoist.UiExtensions.Bridges
{
    public class RequestSyncBridge : Bridge
    {
        [JsonPropertyName("bridgeActionType")]
        public override string BridgeActionType => "request.sync";

        [JsonPropertyName("onSuccessNotification")]
        public Notification OnSuccessNotification { get; set; }

        [JsonPropertyName("onErrorNotification")]
        public Notification OnErrorNotification { get; set; }
    }
}