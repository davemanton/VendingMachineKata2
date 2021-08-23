using Microsoft.Extensions.DependencyInjection;

namespace Application.Infrastructure
{
    public static class ApplicationStartup
    {
        public static IServiceCollection Resolve(IServiceCollection services)
        {
            services
               .AddScoped<IVendingMachine, VendingMachine>()
               .AddScoped<IDetectCoins, CoinDetector>();

            return services;
        }
    }

}