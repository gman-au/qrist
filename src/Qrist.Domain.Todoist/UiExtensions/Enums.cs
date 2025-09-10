using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Qrist.Domain.Todoist.UiExtensions
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum Spacing
    {
        [EnumMember(Value = "default")]
        Default,
        [EnumMember(Value = "none")]
        None,
        [EnumMember(Value = "small")]
        Small,
        [EnumMember(Value = "medium")]
        Medium,
        [EnumMember(Value = "large")]
        Large
    }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum HorizontalAlignment
    {
        [EnumMember(Value = "left")]
        Left,
        [EnumMember(Value = "center")]
        Center,
        [EnumMember(Value = "right")]
        Right
    }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum VerticalContentAlignment
    {
        [EnumMember(Value = "top")]
        Top,
        [EnumMember(Value = "center")]
        Center,
        [EnumMember(Value = "bottom")]
        Bottom
    }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum FontWeight
    {
        [EnumMember(Value = "lighter")]
        Lighter,
        [EnumMember(Value = "default")]
        Default,
        [EnumMember(Value = "bolder")]
        Bolder
    }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum FontSize
    {
        [EnumMember(Value = "default")]
        Default,
        [EnumMember(Value = "small")]
        Small,
        [EnumMember(Value = "medium")]
        Medium,
        [EnumMember(Value = "large")]
        Large,
        [EnumMember(Value = "extraLarge")]
        ExtraLarge
    }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum Color
    {
        [EnumMember(Value = "default")]
        Default,
        [EnumMember(Value = "dark")]
        Dark,
        [EnumMember(Value = "light")]
        Light,
        [EnumMember(Value = "accent")]
        Accent,
        [EnumMember(Value = "good")]
        Good,
        [EnumMember(Value = "warning")]
        Warning,
        [EnumMember(Value = "attention")]
        Attention
    }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum ImageFillMode
    {
        [EnumMember(Value = "cover")]
        Cover,
        [EnumMember(Value = "repeat")]
        Repeat
    }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum ActionStyle
    {
        [EnumMember(Value = "default")]
        Default,
        [EnumMember(Value = "positive")]
        Positive,
        [EnumMember(Value = "destructive")]
        Destructive
    }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum InputStyle
    {
        [EnumMember(Value = "text")]
        Text,
        [EnumMember(Value = "tel")]
        Tel,
        [EnumMember(Value = "email")]
        Email,
        [EnumMember(Value = "url")]
        Url,
        [EnumMember(Value = "search")]
        Search
    }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum ImageSize
    {
        [EnumMember(Value = "auto")]
        Auto,
        [EnumMember(Value = "stretch")]
        Stretch,
        [EnumMember(Value = "small")]
        Small,
        [EnumMember(Value = "medium")]
        Medium,
        [EnumMember(Value = "large")]
        Large
    }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum ChoiceInputStyle
    {
        [EnumMember(Value = "compact")]
        Compact,
        [EnumMember(Value = "expanded")]
        Expanded
    }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum Orientation
    {
        [EnumMember(Value = "vertical")]
        Vertical,
        [EnumMember(Value = "horizontal")]
        Horizontal
    }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum AssociatedInputs
    {
        [EnumMember(Value = "auto")]
        Auto,
        [EnumMember(Value = "none")]
        None,
        [EnumMember(Value = "ignorevalidation")]
        IgnoreValidation
    }
}