using codecrafters_http_server.src.Infrastructure.Interfaces;
using codecrafters_http_server.src.Infrastructure.Networking;
using Microsoft.Extensions.DependencyInjection;

namespace codecrafters_http_server.src.Infrastructure
{
    public static class InfrastructureServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddTransient<INetworkStreamReader, NetworkStreamReader>();

            services.AddTransient<INetworkStreamWriter, NetworkStreamWriter>();

            return services;

        }
    }
}
