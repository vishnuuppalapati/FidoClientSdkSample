using Microsoft.Extensions.DependencyInjection;
using FidoClientSdk.Clients;
using FidoClientSdk.Interfaces;

namespace FidoClientSdk.Extensions
{
    public static class FidoClientServiceCollectionExtensions
    {
        public static IServiceCollection AddFidoClientSdk(this IServiceCollection services, string baseApiUrl)
        {
            services.AddHttpClient<IFidoApiClient, FidoApiClient>(client =>
            {
                client.BaseAddress = new Uri(baseApiUrl);
            });

            return services;
        }
    }
}
