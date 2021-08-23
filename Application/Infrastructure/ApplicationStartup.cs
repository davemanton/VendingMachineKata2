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
               .AddScoped<IStoreCoins, CoinStore>()
               .AddScoped<IDisplayMessages, MachineDisplay>()
                ;

            services
               .AddScoped<IDispenseProducts, ProductDispenser>(factory =>
                {
                    var productStatus = new HashSet<ProductStatus>
                    {
                        new ("a", "cola", 1.00m),
                        new ("b", "chips", 0.50m),
                        new ("c", "candy", 0.65m),
                    };
                    
                    return new ProductDispenser(productStatus, 
                                                factory.GetRequiredService<IDisplayMessages>(),
                                                factory.GetRequiredService<IStoreCoins>());
                });

            return services;
        }
    }

}