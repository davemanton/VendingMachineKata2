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
                ;

            services
               .AddScoped<IDispenseProducts, ProductDispenser>(factory =>
                {
                    var products = new HashSet<Product>
                    {
                        new ("a", "cola", 1.00m),
                        new ("b", "chips", 0.50m),
                        new ("c", "candy", 0.65m),
                    };
                    
                    return new ProductDispenser(products);
                });

            return services;
        }
    }

    public static class DataStartup
    {
        public static IServiceCollection Resolve(IServiceCollection services)
        {
            // For base product dispenser data
            services.AddScoped<ICollection<Product>>(factory => new HashSet<Product>
            {
                new("a", "cola", 1.00m),
                new("b", "chips", 0.50m),
                new("c", "candy", 0.65m),
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