using System.Collections.Generic;
using Domain;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Infrastructure
{
    public static class ApplicationStartup
    {
        public static IServiceCollection Resolve(IServiceCollection services)
        {

            services
               .AddScoped<IVendingMachine, VendingMachine>()
               .AddScoped<IDetectCoins, CoinDetector>()
               .AddScoped<ICoinHandler, CoinHandler>()
               .AddScoped<ITransactionRepository, TransactionRepository>()
               .AddScoped<ICalculateChange, ChangeCalculator>()
               .AddScoped<IDispenseProducts, ProductDispenser>()
                ;

            return services;
        }
    }

    public static class DataStartup
    {
        public static IServiceCollection Resolve(IServiceCollection services)
        {
            // For base product dispenser data
            services.AddScoped<ICollection<ProductStatus>>(factory => new HashSet<ProductStatus>
            {
                new("a", 1.00m, 3),
                new("b", 0.50m, 3),
                new("c", 0.65m, 3),
            });

            // For base coin collection data
            services.AddScoped<ICollection<CoinStatus>>(factory => new HashSet<CoinStatus>()
            {
                new(CoinType.Penny, 0),
                new(CoinType.TwoPence, 0),
                new(CoinType.FivePence, 0),
                new(CoinType.TenPence, 0),
                new(CoinType.TwentyPence, 0),
                new(CoinType.FiftyPence, 0),
                new(CoinType.OnePound, 0),
                new(CoinType.TwoPounds, 0),
            });

            return services;
        }
    }

}