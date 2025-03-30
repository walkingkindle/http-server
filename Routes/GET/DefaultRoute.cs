using System.Text;

namespace codecrafters_http_server.Routes.GET
{
    public class DefaultRoute : IHttpRouteHandler
    {
        public override string _endpoint { get; set; } = "/";
        public override string _method { get; set; } = HttpMethod.GET;

        public override HttpResponse HandleRoute(HttpRequest request)
        {  
            return new HttpResponse("", HttpStatusCodes.GetHttpResponseStatus(HttpStatusCodes.OK), "text/plain", Encoding.UTF8.GetByteCount(""));
        }
    }
}
