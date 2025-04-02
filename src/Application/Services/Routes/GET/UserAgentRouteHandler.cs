using codecrafters_http_server.src.Application.Interfaces;
using codecrafters_http_server.src.Application.Services.Helpers;
using codecrafters_http_server.src.Domain.Entities;
using System.Text;
using HttpMethod = codecrafters_http_server.src.Domain.Entities.HttpMethod;


namespace codecrafters_http_server.src.Application.Services.Routes.GET
{
    public class UserAgentRouteHandler : IHttpRouteHandler
    {
        public override string _route { get; set; } = "/user-agent";
        public override HttpMethod _method { get; set; } = HttpMethod.Create(HttpMethod.GET).Value;

        public override HttpResponse HandleRoute(HttpRequest request)
        {
            if (request.UserAgent is not null)
            {
                return new HttpResponse(
                    new HttpStatusCode(HttpStatusCode.OK),
                    HttpContentType.TextType,
                    null,
                    new HttpResponseBody(HttpHelper.GetBytesFromString(request.UserAgent), Encoding.UTF8.GetByteCount(request.UserAgent)));

            }
            else
            {
                return new HttpResponse(new HttpStatusCode(HttpStatusCode.NotFound),
                    HttpContentType.TextType);
            }

        }
    }
}
