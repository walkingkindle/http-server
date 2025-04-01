using codecrafters_http_server.src.Application.Interfaces;
using codecrafters_http_server.src.Application.Services;
using codecrafters_http_server.src.Application.Services.Helpers;
using codecrafters_http_server.src.Application.Services.Routing;
using codecrafters_http_server.src.Infrastructure.Networking;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace codecrafters_http_server.src.Presentation
{
    public class Program
{
    public static async Task Main(string[] args)
    {
            var builder = Host.CreateDefaultBuilder(args)
                              .ConfigureServices((hostContext, services) =>
                              {
                                  var startup = new Startup();
                                  startup.ConfigureServices(services);
                              });
        var host = builder.Build();

        await StartServer(host,args);
    }

    private static async Task StartServer(IHost host, string[] args)
    {
        var listener = host.Services.GetRequiredService<TcpListener>();
        var routerManager = host.Services.GetRequiredService<IRouteManagerService>();
        var clientHandler = host.Services.GetRequiredService<IClientHandlerService>();

        listener.Start();
        Console.WriteLine("Server is running on port 4221...");

        try
        {
            while (true)
            {
                var handler = await listener.AcceptTcpClientAsync();
                _ = Task.Run(() => clientHandler.HandleClientAsync(handler, args));
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Server error: {ex.Message}");
        }
        finally
        {
            listener.Stop();
        }
    }
}
}
