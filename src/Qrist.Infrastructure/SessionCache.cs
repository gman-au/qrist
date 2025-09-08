using System;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using Qrist.Domain;
using Qrist.Infrastructure.Options;
using Qrist.Interfaces;

namespace Qrist.Infrastructure
{
    public class SessionCache : ISessionCache
    {
        private const int DefaultCacheWindowSeconds = 120;

        private readonly TimeSpan _cacheWindow;

        private readonly IMemoryCache _memoryCacheStates = new MemoryCache(new MemoryCacheOptions());
        private readonly IMemoryCache _memoryCacheIds = new MemoryCache(new MemoryCacheOptions());

        public SessionCache(IOptions<QristConfigurationOptions> optionsAccessor)
        {
            var options =
                optionsAccessor
                    .Value;

            _cacheWindow =
                new TimeSpan(
                    0,
                    0,
                    options?
                        .CacheWindowSeconds
                    ?? DefaultCacheWindowSeconds
                );
        }

        public void Store(Guid id, string state, string qrCodeData, string accessToken = null)
        {
            var sessionStateItem =
                new SessionStateItem
                {
                    State = state,
                    Id = id,
                    QrCodeData = qrCodeData,
                    AccessToken = accessToken
                };

            _memoryCacheStates
                .Set(state, sessionStateItem, _cacheWindow);

            _memoryCacheIds
                .Set(id, sessionStateItem, _cacheWindow);
        }

        public SessionStateItem RetrieveByState(string state)
        {
            if (!_memoryCacheStates
                    .TryGetValue(
                        state,
                        out SessionStateItem sessionStateItem
                    ))
                throw new Exception("Session not found. Your may have timed out or taken too long to respond. Please try again.");

            return sessionStateItem;
        }

        public SessionStateItem RetrieveById(Guid id)
        {
            if (!_memoryCacheIds
                    .TryGetValue(
                        id,
                        out SessionStateItem sessionStateItem
                    ))
                throw new Exception("Session not found. Your may have timed out or taken too long to respond. Please try again.");

            return sessionStateItem;
        }

        public void RemoveByState(string state)
        {
            _memoryCacheStates
                .Remove(state);
        }

        public void RemoveById(Guid id)
        {
            _memoryCacheStates
                .Remove(id);
        }
    }
}