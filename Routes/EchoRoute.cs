
using System.Text;

namespace codecrafters_http_server.Routes
{
    public class EchoRoute : IHttpRouteHandler
    {
        public override string _endpoint { get; set; } = "/echo";
        public override HttpResponse HandleRoute(HttpRequest request)
        {
            string message = request.Endpoint.Replace(_endpoint, "").Replace("/", "").Trim();
            return new HttpResponse(message,"200 OK", "text/plain", Encoding.UTF8.GetByteCount(message));

        }
    }
}
