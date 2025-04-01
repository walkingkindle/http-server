using codecrafters_http_server.src.Domain.Entities;
using CSharpFunctionalExtensions;

namespace codecrafters_http_server.src.Infrastructure.Interfaces
{
    public interface INetworkStreamReader
    {
        public Task<Result<HttpRequest>> GetHttpRequestFromBuffer(string[] args);


    }
}
