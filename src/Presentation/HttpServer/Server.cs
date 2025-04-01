using System.Net;
using System.Net.Sockets;
using System.Text;
using codecrafters_http_server.src.Application.Interfaces;
using codecrafters_http_server.src.Application.Services.Helpers;
using codecrafters_http_server.src.Domain.Entities;
using codecrafters_http_server.src.Infrastructure.Interfaces;
using codecrafters_http_server.src.Infrastructure.Networking;
using CSharpFunctionalExtensions;

public class Server
{
    private readonly IRouteManagerService _routeManagerService;
    private readonly string[] _args;
    private readonly INetworkStreamReader _streamReader;
    public Server(IRouteManagerService routeManagerService, string[] args, INetworkStreamReader streamReader)
    {
        _routeManagerService = routeManagerService;
        _args = args;
        _streamReader = streamReader;
    }

    public async Task StartServer()
    {
        var ipEndPoint = new IPEndPoint(IPAddress.Any, 4221);
        TcpListener listener = new(ipEndPoint);

        listener.Start();
        Console.WriteLine("Server is running on port 4221...");

        try
        {
            while (true)
            {
                TcpClient handler = await listener.AcceptTcpClientAsync();
                _ = Task.Run(() => HandleClient(handler, _args));
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

        private async Task HandleClient(TcpClient handler, string[] args)
        {
            try
            {
                await using NetworkStream stream = handler.GetStream();

                Result<HttpRequest> request = await _streamReader.GetHttpRequestFromBuffer(args);

                if (request.IsFailure)
                {
                    throw new Exception(request.Error);
                }

                HttpResponse? response = _routeManagerService.GetHandler(request.Value)?.HandleRoute(request.Value);

                await stream.WriteAsync(Encoding.UTF8.GetBytes(HttpHelper.BuildHeaders(response)));
                await stream.WriteAsync(Encoding.UTF8.GetBytes(response.ResponseMessage));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Client error: {ex.Message}");
            }
            finally
            {
                handler.Close();
            }
        }
    }
