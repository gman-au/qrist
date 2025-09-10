using System.Text.Json.Serialization;

namespace Qrist.Domain.Todoist.UiExtensions.CardElements
{
    [JsonPolymorphic(TypeDiscriminatorPropertyName = "type")]
    [JsonDerivedType(typeof(TextBlock), typeDiscriminator: "TextBlock")]
    [JsonDerivedType(typeof(ImageElement), typeDiscriminator: "Image")]
    [JsonDerivedType(typeof(RichTextBlock), typeDiscriminator: "RichTextBlock")]
    [JsonDerivedType(typeof(ActionSet), typeDiscriminator: "ActionSet")]
    [JsonDerivedType(typeof(Container), typeDiscriminator: "Container")]
    [JsonDerivedType(typeof(ColumnSet), typeDiscriminator: "ColumnSet")]
    [JsonDerivedType(typeof(Column), typeDiscriminator: "Column")]
    [JsonDerivedType(typeof(InputText), typeDiscriminator: "Input.Text")]
    [JsonDerivedType(typeof(InputDate), typeDiscriminator: "Input.Date")]
    [JsonDerivedType(typeof(InputTime), typeDiscriminator: "Input.Time")]
    [JsonDerivedType(typeof(InputChoiceSet), typeDiscriminator: "Input.ChoiceSet")]
    [JsonDerivedType(typeof(InputToggle), typeDiscriminator: "Input.Toggle")]
    public abstract class CardElement
    {
        [JsonPropertyName("type")]
        public abstract string Type { get; }

        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("spacing")]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public Spacing? Spacing { get; set; }
    }
}