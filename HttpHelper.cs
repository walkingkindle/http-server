using System.Text;

namespace codecrafters_http_server
{
    public class HttpHelper
    {
        public static string BuildHeaders(HttpResponse? response)
        {
            return $"{response.StatusCode}\r\nContent-Type: {response.ContentType}\r\nContent-Length: {response.SizeInBytes}\r\n\r\n"; 
        }
    }
}
