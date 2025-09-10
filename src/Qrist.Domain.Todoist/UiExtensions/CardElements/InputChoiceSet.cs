using System.Collections.Generic;
using System.Text.Json.Serialization;
using Qrist.Domain.Todoist.UiExtensions.Actions;

namespace Qrist.Domain.Todoist.UiExtensions.CardElements
{
    public class InputChoiceSet : CardElement
    {
        [JsonPropertyName("type")]
        public override string Type => "Input.ChoiceSet";

        [JsonPropertyName("label")]
        public string Label { get; set; }

        [JsonPropertyName("isRequired")]
        public bool? IsRequired { get; set; }

        [JsonPropertyName("errorMessage")]
        public string ErrorMessage { get; set; }

        [JsonPropertyName("value")]
        public string Value { get; set; }

        [JsonPropertyName("choices")]
        public List<Choice> Choices { get; set; } = [];

        [JsonPropertyName("isMultiSelect")]
        public bool? IsMultiSelect { get; set; }

        [JsonPropertyName("selectAction")]
        public ActionBase SelectAction { get; set; }

        [JsonPropertyName("style")]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public ChoiceInputStyle? Style { get; set; }

        [JsonPropertyName("orientation")]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public Orientation? Orientation { get; set; }
    }
}