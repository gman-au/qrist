using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Qrist.Domain;
using Qrist.Interfaces;

namespace Qrist.Application
{
    public class QrCodeProcessor(
        IRequestDecoder requestDecoder,
        IEnumerable<IRequestActioner> actioners,
        ILogger<QrCodeProcessor> logger,
        ISessionCache sessionCache
    ) : IQrCodeProcessor
    {
        public async Task<string> GetConfirmationAsync(
            Guid sessionId,
            CancellationToken cancellationToken = default
        )
        {
            var sessionStateItem =
                sessionCache
                    .RetrieveById(sessionId);

            var (request, actioner) =
                await
                    GetRequestAndActionerAsync(sessionStateItem.QrCodeData, cancellationToken);

            return
            await
                actioner
                    .GetConfirmationAsync(request, cancellationToken);
        }

        public async Task ProcessActionAsync(
            Guid sessionId,
            CancellationToken cancellationToken = default
        )
        {
            try
            {
                logger
                    .LogInformation("Received QR code to process {sessionId}", sessionId);

                var sessionStateItem =
                    sessionCache
                        .RetrieveById(sessionId);

                var (request, actioner) =
                    await
                        GetRequestAndActionerAsync(
                            sessionStateItem.QrCodeData,
                            cancellationToken
                        );

                await
                    actioner
                        .ProcessAsync(
                            request,
                            sessionStateItem,
                            cancellationToken
                        );

                logger
                    .LogInformation("Processed QR code successfully");

                // clear cache entries when completed
                sessionCache
                    .RemoveById(sessionId);

                sessionCache
                    .RemoveByState(sessionStateItem.State);
            }
            catch (Exception ex)
            {
                logger
                    .LogError("Error processing QR code: {message}", ex.Message);
            }
        }

        private async Task<Tuple<QrCodeRequest, IRequestActioner>> GetRequestAndActionerAsync(
            string base64QrCode,
            CancellationToken cancellationToken = default)
        {
            var byteData =
                await
                    requestDecoder
                        .ProcessAsync(base64QrCode, cancellationToken);

            var request =
                JsonSerializer
                    .Deserialize<QrCodeRequest>(byteData);

            if (request.Provider == null)
                throw new Exception("Could not identify provider in QR code.");

            var actioner =
                actioners
                    .FirstOrDefault(o => o.IsApplicable(request.Provider));

            if (actioner == null)
                throw new Exception($"Could not identify actioner for QR code of provider type {request.Provider}.");

            return
                new Tuple<QrCodeRequest, IRequestActioner>(
                    request,
                    actioner
                );
        }
    }
}