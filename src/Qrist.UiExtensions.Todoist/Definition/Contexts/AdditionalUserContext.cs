using System.Text.Json.Serialization;

namespace Qrist.UiExtensions.Todoist.Definition.Contexts
{
    public class AdditionalUserContext
    {
        [JsonPropertyName("isPro")]
        public bool IsPro { get; set; }
    }
}