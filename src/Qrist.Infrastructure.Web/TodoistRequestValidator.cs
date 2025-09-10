using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Qrist.Adapters.Todoist.Options;

namespace Qrist.Infrastructure.Web
{
    public class TodoistRequestValidator : ITodoistRequestValidator
    {
        private readonly TodoistConfigurationOptions _options;

        public TodoistRequestValidator(IOptions<TodoistConfigurationOptions> optionsAccessor)
        {
            _options = optionsAccessor.Value;
        }

        private const string TodoistHmacHeader = "x-todoist-hmac-sha256";

        public async Task<bool> IsValidAsync(HttpRequest httpRequest)
        {
            if (httpRequest.Body.CanSeek)
            {
                httpRequest.Body.Position = 0;
            }

            var verificationToken = _options.VerificationToken;

            using var reader = new StreamReader(httpRequest.Body, Encoding.UTF8, leaveOpen: true);
            var requestBody =
                await
                    reader
                        .ReadToEndAsync();

            var requestBodyBytes =
                Encoding
                    .UTF8
                    .GetBytes(requestBody);

            if (!httpRequest.Headers.TryGetValue(TodoistHmacHeader, out var headerValues))
                return false;

            var providedHmac =
                headerValues
                    .FirstOrDefault();

            if (string.IsNullOrEmpty(providedHmac))
                return false;

            // Validate that we have a request body
            if (requestBodyBytes == null || requestBodyBytes.Length == 0)
                return false;

            try
            {
                // Compute the HMAC-SHA256 hash using the verification token as key
                using (var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(verificationToken)))
                {
                    var computedHash = hmac.ComputeHash(requestBodyBytes);
                    var computedHmacBase64 = Convert.ToBase64String(computedHash);

                    // Compare the computed HMAC with the provided one
                    return
                        string
                            .Equals(
                                providedHmac,
                                computedHmacBase64,
                                StringComparison.Ordinal
                            );
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}