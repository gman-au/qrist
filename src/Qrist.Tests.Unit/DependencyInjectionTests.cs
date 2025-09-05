using System;
using Microsoft.Extensions.DependencyInjection;
using Qrist.Api.Host.Injection;
using Qrist.Interfaces;

namespace Qrist.Tests.Unit
{
    internal class Tests
    {
        private readonly TestContext _context = new();

        [Test]
        public void Test_Dependency_Injection()
        {
            _context.ArrangeConfiguration();
            _context.ActBuildServices();
            _context.AssertCanResolveTopLevelServices();
        }

        private class TestContext
        {
            private IServiceProvider _provider;
            private IServiceCollection _services;

            public void ArrangeConfiguration()
            {
                _services =
                    new ServiceCollection();

                _services
                    .AddQristServices();
            }

            public void ActBuildServices() =>
                _provider =
                    _services
                        .BuildServiceProvider();

            public void AssertCanResolveTopLevelServices()
            {
                var healthChecker =
                    _provider
                        .GetService<IHealthChecker>();

                var qrCodeBuilderRequestHandler =
                    _provider
                        .GetService<IQrCodeEncoder>();

                var qrCodeProcessor =
                    _provider
                        .GetService<IQrCodeProcessor>();

                var requestActioners =
                    _provider
                        .GetServices<IRequestActioner>();

                Assert
                    .Multiple(() =>
                    {
                        Assert.That(healthChecker, Is.Not.Null);
                        Assert.That(qrCodeBuilderRequestHandler, Is.Not.Null);
                        Assert.That(qrCodeProcessor, Is.Not.Null);
                        Assert.That(requestActioners, Is.Not.Empty);
                    });
            }
        }
    }
}