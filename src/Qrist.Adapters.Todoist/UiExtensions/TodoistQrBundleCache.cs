using System;
using Microsoft.Extensions.Caching.Memory;
using Qrist.Domain.Todoist.API;

namespace Qrist.Adapters.Todoist.UiExtensions
{
    public class TodoistQrBundleCache : ITodoistQrBundleCache
    {
        private const int DefaultCacheWindowHours = 24;

        private readonly TimeSpan _cacheWindow = new(DefaultCacheWindowHours, 0, 0);

        private readonly IMemoryCache _memoryCacheIds = new MemoryCache(new MemoryCacheOptions());

        public void Store(CreateTodoistTaskApiRequest task, long id)
        {
            _memoryCacheIds
                .Set(
                    id,
                    task,
                    _cacheWindow
                );
        }

        public void Clear(long id)
        {
            _memoryCacheIds
                .Remove(id);
        }

        public CreateTodoistTaskApiRequest RetrieveById(long id)
        {
            if (!_memoryCacheIds
                    .TryGetValue(
                        id,
                        out CreateTodoistTaskApiRequest cachedRequest
                    ))
                return new CreateTodoistTaskApiRequest
                {
                    Tasks = []
                };

            return cachedRequest;
        }
    }
}