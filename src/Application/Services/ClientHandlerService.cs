using codecrafters_http_server.src.Application.Interfaces;
using codecrafters_http_server.src.Application.Services.Helpers;
using codecrafters_http_server.src.Application.Services.Routing;
using codecrafters_http_server.src.Infrastructure.Networking;
using System.Net.Sockets;
using System.Text;

namespace codecrafters_http_server.src.Application.Services
{
     public class ClientHandlerService:IClientHandlerService
    {
        private readonly RouterManager _routerManager;

        public ClientHandlerService(RouterManager routerManager)
        {
            _routerManager = routerManager;
        }

        public async Task HandleClientAsync(TcpClient handler, string[] args)
        {
            try
            {
                await using var stream = handler.GetStream();
                var reader = new NetworkStreamReader(handler, stream, args);

                var request = await reader.GetHttpRequestFromBuffer();

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

