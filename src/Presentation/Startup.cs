using codecrafters_http_server.src.Application.Services;
using codecrafters_http_server.src.Application.Services.Routing;
using codecrafters_http_server.src.Infrastructure.Networking;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

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

            services.AddSingleton<RouterManager>();

            services.AddScoped<ClientHandlerService>();

        }

    }

}
