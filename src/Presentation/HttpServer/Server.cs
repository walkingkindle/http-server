using System.Net.Sockets;
using System.Text;
using codecrafters_http_server.src.Application.Interfaces;
using codecrafters_http_server.src.Application.Services.Helpers;
using codecrafters_http_server.src.Domain.Entities;
using codecrafters_http_server.src.Infrastructure.Interfaces;
using codecrafters_http_server.src.Presentation.Interfaces;
using CSharpFunctionalExtensions;

public class Server:IServer
{
    private readonly IRouteManagerService _routeManagerService;
    private readonly INetworkStreamReader _streamReader;
    public Server(IRouteManagerService routeManagerService, INetworkStreamReader streamReader)
    {
        _routeManagerService = routeManagerService;
        _streamReader = streamReader;
    }

    public async Task HandleClient(TcpClient handler, NetworkStream stream, string[] args)
    {
        try
        {
            Result<HttpRequest> request = await _streamReader.GetHttpRequestFromBuffer(args, stream, handler);

            if (request.IsFailure)
            {
                throw new Exception(request.Error);
            }

            HttpResponse? response = _routeManagerService.GetHandler(request.Value.Endpoint, request.Value.Method)?.HandleRoute(request.Value);

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
