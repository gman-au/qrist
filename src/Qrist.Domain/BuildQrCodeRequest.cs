using System.Text.Json.Serialization;

namespace Qrist.Domain
{
    public class BuildQrCodeRequest
    {
        [JsonPropertyName("provider")] public string Provider { get; set; }

        [JsonPropertyName("data")] public dynamic Data { get; set; }
    }
}