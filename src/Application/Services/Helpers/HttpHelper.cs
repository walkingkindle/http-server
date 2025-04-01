using codecrafters_http_server.src.Domain.Entities;
using System.IO.Compression;
using System.Text;

namespace codecrafters_http_server.src.Application.Services.Helpers
{
    public class HttpHelper
    {
        public static string BuildHeaders(HttpResponse? response)
        {
            if(response.ContentEncoding != null)
            {
                return $"{response.StatusCode}\r\nContent-Type: {response.ContentType}\r\nContent-Length: {response.SizeInBytes}\r\nContent-Encoding:{response.ContentEncoding}\r\n\r\n"; 
            }
            return $"{response.StatusCode}\r\nContent-Type: {response.ContentType}\r\nContent-Length: {response.SizeInBytes}\r\n\r\n"; 
        }

        public static string CompressStringGzip(string query)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(query);
            using (var memoryStream = new MemoryStream())
            {
                using (var gzipStream = new GZipStream(memoryStream, CompressionLevel.Optimal))
                {
                    gzipStream.Write(bytes, 0, bytes.Length);
                }
                return string.Join(' ',memoryStream.ToArray());
            }
        }
    }
}
