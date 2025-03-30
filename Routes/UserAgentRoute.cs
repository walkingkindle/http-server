
using System.Text;

namespace codecrafters_http_server.Routes
{
    public class UserAgentRoute : IHttpRouteHandler
    {
        public override string _endpoint { get; set; } = "/user-agent";

        public override HttpResponse HandleRoute(HttpRequest request)
        {
            if (request.UserAgent is not null)
            {
                return new HttpResponse(request.UserAgent, HttpStatusCodes.OK, HTTPContentType.TextType, Encoding.UTF8.GetByteCount(request.UserAgent));
            }
            else
            {
                return new HttpResponse("", HttpStatusCodes.NotFound, HTTPContentType.TextType, Encoding.UTF8.GetByteCount(""));
            }

        }
    }
}
