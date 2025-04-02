using codecrafters_http_server.src.Domain.Entities;
using System.Net.Sockets;

namespace codecrafters_http_server.src.Infrastructure.Interfaces
{
    public interface INetworkStreamWriter
    {
        public ValueTask WriteHeadersToStream(HttpResponse response, NetworkStream stream);
        public ValueTask WriteBodyToStream(HttpResponseBody response, NetworkStream stream);


    }
}
