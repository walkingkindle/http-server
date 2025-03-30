using System.Text;

namespace codecrafters_http_server.Routes.GET
{
    public class UserAgentRoute : IHttpRouteHandler
    {
        public override string _endpoint { get; set; } = "/user-agent";
        public override string _method { get; set; } = HttpMethod.GET;

        public override HttpResponse HandleRoute(HttpRequest request)
        {
            if (request.UserAgent is not null)
            {
                return new HttpResponse(request.UserAgent, HttpStatusCodes.GetHttpResponseStatus(HttpStatusCodes.OK), HTTPContentType.TextType, Encoding.UTF8.GetByteCount(request.UserAgent));
            }
            else
            {
                return new HttpResponse("", HttpStatusCodes.GetHttpResponseStatus(HttpStatusCodes.NotFound), HTTPContentType.TextType, Encoding.UTF8.GetByteCount(""));
            }

        }
    }
}
