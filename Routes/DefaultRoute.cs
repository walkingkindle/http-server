using System.Text;

namespace codecrafters_http_server.Routes
{
    public class DefaultRoute : IHttpRouteHandler
    {
        public override string _endpoint { get; set; } = "/";
        public override HttpResponse HandleRoute(HttpRequest request)
        {  
            return new HttpResponse("", "200 OK", "text/plain", Encoding.UTF8.GetByteCount(""));
        }
    }
}
