using codecrafters_http_server.src.Application.Interfaces;
using codecrafters_http_server.src.Domain.Entities;
using System.Text;
using HttpMethod = codecrafters_http_server.src.Domain.Entities.HttpMethod;

namespace codecrafters_http_server.src.Application.Services.Routes.GET
{
    public class DefaultRoute : IHttpRouteHandler
    {
        public override string _route { get; set; } = "/";
        public override Domain.Entities.HttpMethod _method { get; set; } = HttpMethod.Create(HttpMethod.GET).Value;

        public override HttpResponse HandleRoute(HttpRequest request)
        {  
            return new HttpResponse("", HttpStatusCodes.GetHttpResponseStatus(HttpStatusCodes.OK), "text/plain", Encoding.UTF8.GetByteCount(""));
        }
    }
}
