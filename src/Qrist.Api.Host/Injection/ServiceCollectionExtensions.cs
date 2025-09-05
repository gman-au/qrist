using Microsoft.Extensions.DependencyInjection;
using Qrist.Adapters.Todoist;
using Qrist.Api.Host.Health;
using Qrist.Interfaces;

namespace Qrist.Api.Host.Injection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddQristServices(this IServiceCollection services)
        {
            services
                .AddTransient<ITodoistAdapter, TodoistAdapter>()
                .AddTransient<IHealthChecker, HealthChecker>();

            return services;
        }
    }
}