using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Qrist.Adapters.Todoist;
using Qrist.Adapters.Todoist.Authorisation;
using Qrist.Adapters.Todoist.Options;
using Qrist.Application;
using Qrist.Infrastructure;
using Qrist.Infrastructure.Compression.Brotli;
using Qrist.Infrastructure.Options;
using Qrist.Infrastructure.QrCode.Encoding;
using Qrist.Infrastructure.QrCode.Production;
using Qrist.Infrastructure.Table.Azure;
using Qrist.Infrastructure.Table.Azure.Options;
using Qrist.Interfaces;
using Qrist.UiExtensions.Todoist;

namespace Qrist.Injection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddQristServices(
            this IServiceCollection services,
            IConfigurationRoot configuration)
        {
            services
                .AddTransient<IQristApplication, QristApplication>()
                .AddTransient<IRequestEncoder, RequestEncoder>()
                .AddTransient<IRequestDecoder, RequestDecoder>()
                .AddTransient<IQrCodeProcessor, QrCodeProcessor>()
                .AddTransient<IHealthChecker, HealthChecker>()
                .AddTransient<ICodeGenerator, CodeGenerator>();

            services
                .AddTransient<ICompressor, BrotliCompressor>()
                .AddTransient<IQristUrlBuilder, QristUrlBuilder>()
                .AddTransient<IQrCodeGenerator, QrCodeGenerator>();

            services
                .AddTransient<IRequestActioner, TodoistQrCodeActioner>()
                .AddTransient<ITodoistAuthoriser, TodoistAuthoriser>()
                .AddTransient<ITodoistCardHandler, TodoistCardHandler>();

            services
                .AddTransient<IKeyValueStorage, AzureTableStorage>();

            services
                .AddSingleton<ISessionCache, SessionCache>();

            services
                .Configure<QristConfigurationOptions>(
                    configuration
                        .GetSection(nameof(QristConfigurationOptions))
                );

            services
                .Configure<TodoistConfigurationOptions>(
                    configuration
                        .GetSection(nameof(TodoistConfigurationOptions))
                );

            services
                .Configure<AzureTableStorageConfigurationOptions>(
                    configuration
                        .GetSection(nameof(AzureTableStorageConfigurationOptions))
                );

            services
                .AddLogging(o => o.AddConsole());

            return services;
        }
    }
}