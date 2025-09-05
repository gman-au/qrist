using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Qrist.Adapters.Todoist;
using Qrist.Api.Host.Infrastructure;
using Qrist.Infrastructure.Brotli;
using Qrist.Interfaces;

namespace Qrist.Api.Host.Injection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddQristServices(this IServiceCollection services)
        {
            services
                .AddTransient<IQristCodeBuilder, TodoistQristCodeBuilder>()
                .AddTransient<IQrCodeBuilderRequestHandler, QrCodeBuilderRequestHandler>()
                .AddTransient<IHealthChecker, HealthChecker>();

            services
                .AddTransient<ICompressor, BrotliCompressor>();
            services
                .AddLogging(o => o.AddConsole());

            return services;
        }
    }
}