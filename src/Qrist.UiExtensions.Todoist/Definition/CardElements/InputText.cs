using System.Text.Json.Serialization;
using Qrist.UiExtensions.Todoist.Definition.Actions;

namespace Qrist.UiExtensions.Todoist.Definition.CardElements
{
    public class InputText : CardElement
    {
        [JsonPropertyName("type")]
        public override string Type => "Input.Text";

        [JsonPropertyName("placeholder")]
        public string Placeholder { get; set; }

        [JsonPropertyName("value")]
        public string Value { get; set; }

        [JsonPropertyName("inlineAction")]
        public ActionBase InlineAction { get; set; }

        [JsonPropertyName("style")]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public InputStyle? Style { get; set; }

        [JsonPropertyName("maxLength")]
        public int? MaxLength { get; set; }

        [JsonPropertyName("isMultiline")]
        public bool? IsMultiline { get; set; }

        [JsonPropertyName("label")]
        public string Label { get; set; }

        [JsonPropertyName("isRequired")]
        public bool? IsRequired { get; set; }

        [JsonPropertyName("errorMessage")]
        public string ErrorMessage { get; set; }
    }
}