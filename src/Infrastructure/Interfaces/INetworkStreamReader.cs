using codecrafters_http_server.src.Domain.Entities;
using CSharpFunctionalExtensions;
using System.Net.Sockets;

namespace codecrafters_http_server.src.Infrastructure.Interfaces
{
    public interface INetworkStreamReader
    {
        public Task<Result<HttpRequest>> GetHttpRequestFromBuffer(string[] args, NetworkStream stream, TcpClient handler);


    }
}
