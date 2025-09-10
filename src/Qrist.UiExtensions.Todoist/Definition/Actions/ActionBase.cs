using System.Text.Json.Serialization;

namespace Qrist.UiExtensions.Todoist.Definition.Actions
{
    [JsonPolymorphic(TypeDiscriminatorPropertyName = "type")]
    [JsonDerivedType(typeof(ActionSubmit), typeDiscriminator: "Action.Submit")]
    [JsonDerivedType(typeof(ActionOpenUrl), typeDiscriminator: "Action.OpenUrl")]
    [JsonDerivedType(typeof(ActionClipboard), typeDiscriminator: "Action.Clipboard")]
    public abstract class ActionBase
    {
        [JsonPropertyName("type")]
        public abstract string Type { get; }

        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("iconUrl")]
        public string IconUrl { get; set; }

        [JsonPropertyName("style")]
        public ActionStyle? Style { get; set; }
    }
}