using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Qrist.Adapters.Todoist.Options;
using Qrist.Domain.Todoist;
using Qrist.Domain.Todoist.API;
using Qrist.Interfaces;

namespace Qrist.Adapters.Todoist.Authorisation
{
    public class TodoistAuthoriser(
        ILogger<TodoistAuthoriser> logger,
        ISessionCache sessionCache,
        IOptions<TodoistConfigurationOptions> optionsAccessor,
        ICodeGenerator codeGenerator) : ITodoistAuthoriser
    {
        private const string Scopes = "data:read_write";
        private readonly TodoistConfigurationOptions _options = optionsAccessor.Value;

        public async Task<string> GetRedirectUrlAsync(
            string qrCodeData,
            CancellationToken cancellationToken = default)
        {
            var clientId =
                _options?
                    .ClientId
                ?? throw new Exception($"{nameof(_options.ClientId)} not configured");

            var authEndpoint =
                _options?
                    .AuthRequestEndpoint
                ?? throw new Exception($"{nameof(_options.AuthRequestEndpoint)} not configured");

            var state =
                codeGenerator
                    .Generate();

            var id =
                Guid
                    .NewGuid();

            sessionCache
                .Store(
                    TodoistConstants.Provider,
                    id,
                    state,
                    qrCodeData
                );

            var url = $"{authEndpoint}?client_id={clientId}&scope={Scopes}&state={state}";

            return url;
        }

        public async Task<Guid?> RetrieveAndCacheAccessTokenByIdAsync(
            string code,
            string state,
            CancellationToken cancellationToken = default)
        {
            try
            {
                // get existing session
                var sessionStateItem =
                    sessionCache
                        .RetrieveByState(state);

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
                            .ReadFromJsonAsync<TodoistAccessTokenApiResponse>(cancellationToken);

                // update session cache
                sessionCache
                    .Store(
                        TodoistConstants.Provider,
                        sessionStateItem.Id,
                        sessionStateItem.State,
                        sessionStateItem.QrCodeData,
                        accessTokenResponse.AccessToken
                    );

                return
                    sessionStateItem
                        .Id;
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