using System.Text.Json.Serialization;

namespace Qrist.UiExtensions.Todoist.Definition.CardElements
{
    public class InputDate : CardElement
    {
        [JsonPropertyName("type")]
        public override string Type => "Input.Date";

        [JsonPropertyName("label")]
        public string Label { get; set; }

        [JsonPropertyName("isRequired")]
        public bool? IsRequired { get; set; }

        [JsonPropertyName("errorMessage")]
        public string ErrorMessage { get; set; }

        [JsonPropertyName("value")]
        public string Value { get; set; }

        [JsonPropertyName("min")]
        public string Min { get; set; }

        [JsonPropertyName("max")]
        public string Max { get; set; }
    }
}