using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Qrist.Adapters.Todoist;
using Qrist.Adapters.Todoist.Options;
using Qrist.Application;
using Qrist.Infrastructure;
using Qrist.Infrastructure.Compression.Brotli;
using Qrist.Infrastructure.QrCode.Encoding;
using Qrist.Infrastructure.QrCode.Production;
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
                .AddTransient<IQrCodeEncoder, QrCodeEncoder>()
                .AddTransient<IQrCodeDecoder, QrCodeDecoder>()
                .AddTransient<IQrCodeProcessor, QrCodeProcessor>()
                .AddTransient<IHealthChecker, HealthChecker>()
                .AddTransient<ICodeGenerator, CodeGenerator>();

            services
                .AddTransient<ICompressor, BrotliCompressor>()
                .AddTransient<IQrCodeGenerator, QrCodeGenerator>();

            services
                .AddTransient<IRequestActioner, TodoistQrCodeActioner>()
                .AddTransient<ITodoistAuthoriser, TodoistAuthoriser>();

            services
                .Configure<TodoistConfigurationOptions>(
                    configuration
                        .GetSection(nameof(TodoistConfigurationOptions))
                );

            services
                .AddLogging(o => o.AddConsole());

            return services;
        }
    }
}