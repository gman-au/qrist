using System.Text.Json.Serialization;

namespace Qrist.Domain.Todoist.UiExtensions.Bridges
{
    [JsonPolymorphic(TypeDiscriminatorPropertyName = "bridgeActionType")]
    [JsonDerivedType(typeof(DisplayNotificationBridge), typeDiscriminator: "display.notification")]
    [JsonDerivedType(typeof(ComposerAppendBridge), typeDiscriminator: "composer.append")]
    [JsonDerivedType(typeof(RequestSyncBridge), typeDiscriminator: "request.sync")]
    [JsonDerivedType(typeof(FinishedBridge), typeDiscriminator: "finished")]
    public abstract class Bridge
    {
        [JsonPropertyName("bridgeActionType")]
        public abstract string BridgeActionType { get; }
    }
}