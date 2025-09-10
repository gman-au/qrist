using System.Text.Json.Serialization;
using Qrist.Domain.Todoist.UiExtensions.Actions;

namespace Qrist.Domain.Todoist.UiExtensions.CardElements
{
    public class InputToggle : CardElement
    {
        [JsonPropertyName("type")]
        public override string Type => "Input.Toggle";

        [JsonPropertyName("title")]
        public string Title { get; set; } = string.Empty;

        [JsonPropertyName("value")]
        public string Value { get; set; }

        [JsonPropertyName("wrap")]
        public bool? Wrap { get; set; }

        [JsonPropertyName("label")]
        public string Label { get; set; }

        [JsonPropertyName("isRequired")]
        public bool? IsRequired { get; set; }

        [JsonPropertyName("errorMessage")]
        public string ErrorMessage { get; set; }

        [JsonPropertyName("selectAction")]
        public ActionBase SelectAction { get; set; }
    }
}