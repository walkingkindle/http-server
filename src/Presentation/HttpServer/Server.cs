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
    private readonly INetworkStreamWriter _streamWriter;
    public Server(IRouteManagerService routeManagerService, INetworkStreamReader streamReader,INetworkStreamWriter streamWriter)
    {
        _routeManagerService = routeManagerService;
        _streamReader = streamReader;
        _streamWriter = streamWriter;
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

            await _streamWriter.WriteHeadersToStream(response, stream);
            await _streamWriter.WriteBodyToStream(response.HttpResponseBody, stream);
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
