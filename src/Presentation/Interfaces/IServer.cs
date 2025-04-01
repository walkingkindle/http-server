using System.Net.Sockets;

namespace codecrafters_http_server.src.Presentation.Interfaces
{
    public interface IServer
    {
        Task HandleClient(TcpClient handler, NetworkStream stream, string[] args);
    }
}
