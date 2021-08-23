using System;
using Application.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Tests
{
    public static class TestDependencyResolver
    {
        public static IServiceProvider Resolve()
        {
            var services = new ServiceCollection();

            ApplicationStartup.Resolve(services);

            return services.BuildServiceProvider();
        }
    }
}