using System;
using System.Web;
using Microsoft.Extensions.Options;
using Qrist.Infrastructure.Options;
using Qrist.Interfaces;

namespace Qrist.Infrastructure.QrCode.Encoding
{
    public class QristUrlBuilder(IOptions<QristConfigurationOptions> optionsAccessor) : IQristUrlBuilder
    {
        private const string ProcessCodeRequestPath = "ProcessCode";
        private readonly QristConfigurationOptions _options = optionsAccessor.Value;

        public string BuildFullUrl(string provider, string encodedRequestData)
        {
            var baseUrl =
                _options?
                    .BaseUrl ??
                throw new Exception("Base URL is not configured.");

            encodedRequestData =
                HttpUtility
                    .UrlEncode(encodedRequestData);

            return
                $"{baseUrl}/{provider?.ToLower()}/{ProcessCodeRequestPath.ToLower()}?code={encodedRequestData}";
        }
    }
}