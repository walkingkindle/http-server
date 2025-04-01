using codecrafters_http_server.src.Application.Interfaces;
using codecrafters_http_server.src.Application.Services.Helpers;
using codecrafters_http_server.src.Application.Services.Routing;
using codecrafters_http_server.src.Infrastructure.Interfaces;
using codecrafters_http_server.src.Infrastructure.Networking;
using System.Net.Sockets;
using System.Text;

namespace codecrafters_http_server.src.Application.Services
{
     public class ClientHandlerService:IClientHandlerService
    {
        private readonly RouteManagerService _routerManager;
        private readonly INetworkStreamReader _streamReader;

        public ClientHandlerService(RouteManagerService routerManager, INetworkStreamReader streamReader)
        {
            _routerManager = routerManager;
            _streamReader = streamReader;
        }

        public async Task HandleClientAsync(TcpClient handler, string[] args)
        {
            try
            {
                await using var stream = handler.GetStream();

                var request = await _streamReader.GetHttpRequestFromBuffer(args);

                if (request.IsFailure)
                {
                    throw new Exception(request.Error);
                }

                var response = _routerManager.GetHandler(request.Value)?.HandleRoute(request.Value);

                await stream.WriteAsync(Encoding.UTF8.GetBytes(HttpHelper.BuildHeaders(response)));
                await stream.WriteAsync(Encoding.UTF8.GetBytes(response.ResponseMessage));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Client handling error: {ex.Message}");
            }
            finally
            {
                handler.Close();
            }
        }
    }
}

