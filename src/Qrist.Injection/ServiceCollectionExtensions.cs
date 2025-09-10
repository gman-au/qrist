using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Qrist.Adapters.Todoist.API;
using Qrist.Adapters.Todoist.Authorisation;
using Qrist.Adapters.Todoist.Options;
using Qrist.Adapters.Todoist.UiExtensions;
using Qrist.Adapters.Todoist.UiExtensions.Handlers;
using Qrist.Application;
using Qrist.Infrastructure;
using Qrist.Infrastructure.Compression.Brotli;
using Qrist.Infrastructure.Options;
using Qrist.Infrastructure.QrCode.Encoding;
using Qrist.Infrastructure.QrCode.Production;
using Qrist.Infrastructure.Table.Azure;
using Qrist.Infrastructure.Table.Azure.Options;
using Qrist.Interfaces;

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
                .AddTransient<IRequestActioner, TodoistApiQrCodeActioner>()
                .AddTransient<ITodoistAuthoriser, TodoistAuthoriser>()
                .AddTransient<ITodoistCardHandler, TodoistCardHandler>();

            services
                .AddTransient<ITodoistActionHandler, InitialRequestHandler>()
                .AddTransient<ITodoistActionHandler, AddToBundleHandler>()
                .AddTransient<ITodoistActionHandler, ClearBundleHandler>()
                .AddTransient<ITodoistActionHandler, ConfirmClearBundleHandler>()
                .AddTransient<ITodoistActionHandler, ConfirmGenerateQrCodeHandler>()
                .AddTransient<ITodoistActionHandler, CancelConfirmationHandler>();

            services
                .AddTransient<IKeyValueStorage, AzureTableStorage>();

            services
                .AddSingleton<ISessionCache, SessionCache>()
                .AddSingleton<ITodoistQrBundleCache, TodoistQrBundleCache>();

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