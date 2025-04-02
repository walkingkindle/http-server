using System.Text;

namespace codecrafters_http_server.src.Domain.Entities
{
    public class HttpResponseBody
    {
        public HttpResponseBody(byte[] responseBody, int byteCount)
        {
            ResponseBody = responseBody;
            ByteCount = byteCount;
        }

        public byte[] ResponseBody { get; set; }

        public int ByteCount { get; set; }
    }
}
