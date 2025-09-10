using System.Text.Json.Serialization;

namespace Qrist.Domain.Todoist.UiExtensions.Contexts
{
    public class AdditionalUserContext
    {
        [JsonPropertyName("isPro")]
        public bool IsPro { get; set; }
    }
}