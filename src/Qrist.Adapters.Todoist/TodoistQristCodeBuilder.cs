using System;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Qrist.Domain;
using Qrist.Interfaces;

namespace Qrist.Adapters.Todoist
{
    public class TodoistQristCodeBuilder : IQristCodeBuilder
    {
        private const string TodoistProvider = "TODOIST";

        private readonly ILogger<TodoistQristCodeBuilder> _logger;
        private readonly ICompressor _compressor;

        public TodoistQristCodeBuilder(
            ILogger<TodoistQristCodeBuilder> logger,
            ICompressor compressor)
        {
            _logger = logger;
            _compressor = compressor;
        }

        public bool IsApplicable(string provider) =>
            string
                .Equals(
                    provider,
                    TodoistProvider,
                    StringComparison.InvariantCultureIgnoreCase
                );

        public async Task<byte[]> GenerateQrCodeAsync(
            BuildQrCodeRequest request,
            CancellationToken cancellationToken = default
        )
        {
            if (request == null)
                throw new Exception("Request contains no data.");

            var jsonRequest =
                JsonSerializer
                    .Serialize(request);

            // minify?

            var byteData =
                Encoding
                    .Default
                    .GetBytes(jsonRequest);

            _logger
                .LogDebug("Original request size: {size}", byteData.Length);

            // compress

            byteData =
                await
                    _compressor
                        .CompressAsync(byteData, cancellationToken);

            _logger
                .LogDebug("Compressed request size: {size}", byteData.Length);

            return byteData;
        }
    }
}