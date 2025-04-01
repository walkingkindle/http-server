using codecrafters_http_server.src.Application;
using codecrafters_http_server.src.Infrastructure;
using codecrafters_http_server.src.Presentation.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System.Net;
using System.Net.Sockets;

namespace codecrafters_http_server.src.Presentation
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<TcpListener>(sp =>
            {
                var ipEndPoint = new IPEndPoint(IPAddress.Any, 4221);
                var listener = new TcpListener(ipEndPoint);
                return listener;
            });

            services.AddScoped<IServer,Server>();

            services.AddApplicationServices();

            services.AddInfrastructureServices();

        }

    }

}
