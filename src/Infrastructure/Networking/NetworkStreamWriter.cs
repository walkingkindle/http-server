using codecrafters_http_server.src.Domain.Entities;
using codecrafters_http_server.src.Infrastructure.Interfaces;
using System.Net.Sockets;
using System.Text;

namespace codecrafters_http_server.src.Infrastructure.Networking
{
    public class NetworkStreamWriter : INetworkStreamWriter
    {
        public async ValueTask WriteHeadersToStream(HttpResponse response, NetworkStream stream)
        {
            string headers;
            if (response.ContentEncoding != null)
            {
                headers = $"{response.StatusCode.Value}\r\nContent-Type: {response.ContentType}\r\nContent-Length: {response.HttpResponseBody.ByteCount}\r\nContent-Encoding:{response.ContentEncoding}\r\n\r\n";
            }
            else
            {
                headers = $"{response.StatusCode.Value}\r\nContent-Type: {response.ContentType}\r\nContent-Length: {(response.HttpResponseBody == null ? 0 :response.HttpResponseBody.ByteCount)}\r\n\r\n";
            }

            await stream.WriteAsync(Encoding.UTF8.GetBytes(headers));
        }
        public async ValueTask WriteBodyToStream(HttpResponseBody response, NetworkStream stream)
        {
            if(response is null)
            {
                response = new HttpResponseBody(Encoding.UTF8.GetBytes(""), 0);
            }
            await stream.WriteAsync(response.ResponseBody);
        }
    }
}
