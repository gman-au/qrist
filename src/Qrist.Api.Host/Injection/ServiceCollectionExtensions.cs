using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Qrist.Adapters.Todoist;
using Qrist.Api.Host.Infrastructure;
using Qrist.Infrastructure.Compression.Brotli;
using Qrist.Infrastructure.QrCode.Encoding;
using Qrist.Infrastructure.QrCode.Production;
using Qrist.Interfaces;

namespace Qrist.Api.Host.Injection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddQristServices(this IServiceCollection services)
        {
            services
                .AddTransient<IQrCodeEncoder, QrCodeEncoder>()
                .AddTransient<IQrCodeDecoder, QrCodeDecoder>()
                .AddTransient<IQrCodeProcessor, QrCodeProcessor>()
                .AddTransient<IHealthChecker, HealthChecker>();

            services
                .AddTransient<ICompressor, BrotliCompressor>()
                .AddTransient<IQrCodeGenerator, QrCodeGenerator>();

            services
                .AddTransient<IRequestActioner, TodoistQrCodeActioner>();

            services
                .AddLogging(o => o.AddConsole());

            return services;
        }
    }
}