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
                    
                    return new ProductDispenser(products, 
                                                factory.GetRequiredService<ITransactionRepository>()
                                                );
                });

            return services;
        }
    }

}