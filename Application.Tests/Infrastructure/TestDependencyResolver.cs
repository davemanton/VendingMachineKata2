using System;
using System.Collections.Generic;
using Application.Infrastructure;
using Domain;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Tests
{
    public static class TestDependencyResolver
    {
        public static IServiceProvider Resolve(ICollection<CoinStatus>? overrideStatus = null)
        {
            var services = new ServiceCollection();

            ApplicationStartup.Resolve(services);

            services.AddScoped<ICollection<Product>>(factory => new HashSet<Product>
            {
                new("a", "cola", 1.00m),
                new("b", "chips", 0.50m),
                new("c", "candy", 0.65m),
            });

        
            services.AddScoped<ICollection<CoinStatus>>(factory 
                => overrideStatus ?? new HashSet<CoinStatus>()
            {
                new(CoinType.Penny, 5),
                new(CoinType.TwoPence, 5),
                new(CoinType.FivePence, 5),
                new(CoinType.TenPence, 5),
                new(CoinType.TwentyPence, 5),
                new(CoinType.FiftyPence, 5),
                new(CoinType.OnePound, 5),
                new(CoinType.TwoPounds, 5),
            });

            return services.BuildServiceProvider();
        }
    }
}