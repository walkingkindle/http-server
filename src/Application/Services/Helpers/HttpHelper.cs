using codecrafters_http_server.src.Domain.Entities;
using System.IO.Compression;
using System.Text;

namespace codecrafters_http_server.src.Application.Services.Helpers
{
    public class HttpHelper
    {

        public static byte[] CompressStringGzip(string query)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(query);
            using (var memoryStream = new MemoryStream())
            {
                using (var gzipStream = new GZipStream(memoryStream, CompressionLevel.Optimal))
                {
                    gzipStream.Write(bytes, 0, bytes.Length);
                }
                return memoryStream.ToArray();
            }
        }

        public static byte[] GetBytesFromString(string value)
        {
            return Encoding.UTF8.GetBytes(value);
        }
    }
}
