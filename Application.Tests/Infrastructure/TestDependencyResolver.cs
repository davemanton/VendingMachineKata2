using System;
using System.Collections.Generic;
using Application.Infrastructure;
using Domain;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Tests
{
    public static class TestDependencyResolver
    {
        public static IServiceProvider Resolve(ICollection<ProductStatus>? overrideProductStatus = null, ICollection<CoinStatus>? overrideCoinStatus = null)
        {
            var services = new ServiceCollection();

            ApplicationStartup.Resolve(services);

            services.AddScoped<ICollection<ProductStatus>>(factory 
                => overrideProductStatus ?? new HashSet<ProductStatus>
            {
                new("a", 1.00m, 3),
                new("b", 0.50m, 3),
                new("c", 0.65m, 3),
            });

        
            services.AddScoped<ICollection<CoinStatus>>(factory 
                => overrideCoinStatus ?? new HashSet<CoinStatus>()
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