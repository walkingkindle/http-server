using System.Net.Sockets;

namespace codecrafters_http_server.src.Application.Interfaces
{
    public interface IClientHandlerService
    {
        public Task HandleClientAsync(TcpClient handler, string[] args);
    }
}
