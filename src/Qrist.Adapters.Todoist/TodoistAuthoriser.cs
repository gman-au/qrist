using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Qrist.Adapters.Todoist.Definition;
using Qrist.Adapters.Todoist.Options;
using Qrist.Interfaces;

namespace Qrist.Adapters.Todoist
{
    public class TodoistAuthoriser(
        ILogger<TodoistAuthoriser> logger,
        IOptions<TodoistConfigurationOptions> optionsAccessor,
        ICodeGenerator codeGenerator) : ITodoistAuthoriser
    {
        private const string Scopes = "data:read_write";
        private readonly TodoistConfigurationOptions _options = optionsAccessor.Value;

        public async Task<string> GetRedirectUrlAsync(CancellationToken cancellationToken = default)
        {
            var clientId = _options?.ClientId ?? throw new Exception($"{nameof(_options.ClientId)} not configured");
            var authEndpoint = _options?.AuthRequestEndpoint ??
                               throw new Exception($"{nameof(_options.AuthRequestEndpoint)} not configured");

            var state =
                codeGenerator
                    .Generate();

            var url = $"{authEndpoint}?client_id={clientId}&scope={Scopes}&state={state}";

            return url;
        }

        public async Task<string> GetAccessTokenAsync(
            string code,
            CancellationToken cancellationToken = default)
        {
            try
            {
                var client = new HttpClient();

                client.BaseAddress =
                    new Uri(
                        _options?
                            .TokenRequestEndpoint ??
                        throw new Exception($"{nameof(_options.TokenRequestEndpoint)} not configured")
                    );

                var request =
                    new HttpRequestMessage(
                        HttpMethod.Post,
                        ""
                    );

                request
                    .Headers
                    .Add(
                        "ContentType",
                        "application/x-www-form-urlencoded"
                    );

                var formData = new List<KeyValuePair<string, string>>
                {
                    new(
                        "client_id",
                        _options.ClientId
                    ),
                    new(
                        "client_secret",
                        _options.ClientSecret
                    ),
                    new(
                        "code",
                        code
                    )
                };

                request.Content = new FormUrlEncodedContent(formData);

                var httpResponse =
                    await
                        client
                            .SendAsync(request, cancellationToken);

                httpResponse
                    .EnsureSuccessStatusCode();

                var accessTokenResponse =
                    await
                        httpResponse
                            .Content
                            .ReadFromJsonAsync<TodoistAccessTokenResponse>(cancellationToken);

                return
                    accessTokenResponse
                        .AccessToken;
            }
            catch (Exception e)
            {
                logger
                    .LogError("There was an error getting the access token: {message}", e.Message);

                throw;
            }
        }
    }
}