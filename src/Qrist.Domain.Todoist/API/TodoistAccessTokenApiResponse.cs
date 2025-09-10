using System.Text.Json.Serialization;

namespace Qrist.Domain.Todoist.API
{
    public class TodoistAccessTokenApiResponse
    {
        [JsonPropertyName("access_token")] public string AccessToken { get; set; }

        [JsonPropertyName("token_type")] public string TokenType { get; set; }
    }
}