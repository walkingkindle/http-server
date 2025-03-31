using System.Text;

namespace codecrafters_http_server.Routes.GET
{
    public class DefaultRoute : IHttpRouteHandler
    {
        public override string _route { get; set; } = "/";
        public override HttpMethod _method { get; set; } = HttpMethod.Create(HttpMethod.GET).Value;

        public override HttpResponse HandleRoute(HttpRequest request)
        {  
            return new HttpResponse("", HttpStatusCodes.GetHttpResponseStatus(HttpStatusCodes.OK), "text/plain", Encoding.UTF8.GetByteCount(""));
        }
    }
}
