using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using Qrist.Domain.Todoist.UiExtensions.Actions;

namespace Qrist.Domain.Todoist.UiExtensions
{
    [JsonConverter(typeof(InlineConverter))]
    public abstract class Inline
    {
    }

    public class InlineText : Inline
    {
        public string Text { get; set; } = string.Empty;
    }

    public class TextRun : Inline
    {
        [JsonPropertyName("type")]
        public string Type => "TextRun";

        [JsonPropertyName("text")]
        public string Text { get; set; } = string.Empty;

        [JsonPropertyName("color")]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public Color? Color { get; set; }

        [JsonPropertyName("size")]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public FontSize? Size { get; set; }

        [JsonPropertyName("isSubtle")]
        public bool? IsSubtle { get; set; }

        [JsonPropertyName("weight")]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public FontWeight? Weight { get; set; }

        [JsonPropertyName("selectAction")]
        public ActionBase SelectAction { get; set; }
    }

    public class InlineConverter : JsonConverter<Inline>
    {
        public override Inline Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.String)
            {
                return new InlineText { Text = reader.GetString() ?? string.Empty };
            }

            if (reader.TokenType == JsonTokenType.StartObject)
            {
                using var doc = JsonDocument.ParseValue(ref reader);
                if (doc.RootElement.TryGetProperty("type", out var typeProperty) &&
                    typeProperty.GetString() == "TextRun")
                {
                    return JsonSerializer.Deserialize<TextRun>(doc.RootElement, options) ?? new TextRun();
                }
            }

            throw new JsonException("Unable to deserialize Inline");
        }

        public override void Write(Utf8JsonWriter writer, Inline value, JsonSerializerOptions options)
        {
            switch (value)
            {
                case InlineText inlineText:
                    writer.WriteStringValue(inlineText.Text);
                    break;
                case TextRun textRun:
                    JsonSerializer.Serialize(writer, textRun, options);
                    break;
                default:
                    throw new JsonException($"Unknown Inline type: {value.GetType()}");
            }
        }
    }
}