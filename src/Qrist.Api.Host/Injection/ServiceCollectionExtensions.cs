using Microsoft.Extensions.DependencyInjection;
using Qrist.Adapters.Todoist;
using Qrist.Api.Host.Infrastructure;
using Qrist.Interfaces;

namespace Qrist.Api.Host.Injection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddQristServices(this IServiceCollection services)
        {
            services
                .AddTransient<ITodoistAdapter, TodoistAdapter>()
                .AddTransient<IQristCodeBuilder, TodoistQristCodeBuilder>()
                .AddTransient<IQrCodeBuilderRequestHandler, QrCodeBuilderRequestHandler>()
                .AddTransient<IHealthChecker, HealthChecker>();

            return services;
        }
    }
}