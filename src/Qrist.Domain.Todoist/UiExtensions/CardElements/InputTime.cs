using System.Text.Json.Serialization;

namespace Qrist.Domain.Todoist.UiExtensions.CardElements
{
    public class InputTime : CardElement
    {
        [JsonPropertyName("type")]
        public override string Type => "Input.Time";

        [JsonPropertyName("label")]
        public string Label { get; set; }

        [JsonPropertyName("isRequired")]
        public bool? IsRequired { get; set; }

        [JsonPropertyName("errorMessage")]
        public string ErrorMessage { get; set; }

        [JsonPropertyName("value")]
        public string Value { get; set; }
    }
}