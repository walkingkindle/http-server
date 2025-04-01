using codecrafters_http_server.src.Application.Interfaces;
using codecrafters_http_server.src.Application.Services.Helpers;
using codecrafters_http_server.src.Domain.Entities;
using System.Text;
using HttpMethod = codecrafters_http_server.src.Domain.Entities.HttpMethod;


namespace codecrafters_http_server.src.Application.Services.Routes.GET
{
    public class EchoRouteHandler : IHttpRouteHandler
    {
        public override string _route { get; set; } = "/echo";

        public override HttpMethod _method { get; set; } = HttpMethod.Create(HttpMethod.GET).Value;

        public override HttpResponse HandleRoute(HttpRequest request)
        {
            if (!request.AcceptEncoding.IsNullOrEmpty())
            {
               string encodings = string.Join(',', request.AcceptEncoding.Select(p=> p.Value));
               return new HttpResponse(request.Endpoint.Query, HttpStatusCodes.GetHttpResponseStatus(HttpStatusCodes.OK), HttpContentType.Create(HttpContentType.TextType).Value.ToString(), Encoding.UTF8.GetByteCount(request.Endpoint.Query),encodings);
            }
            return new HttpResponse(request.Endpoint.Query, HttpStatusCodes.GetHttpResponseStatus(HttpStatusCodes.OK), HttpContentType.Create(HttpContentType.TextType).Value.ToString(), Encoding.UTF8.GetByteCount(request.Endpoint.Query));

            
        }
    }
}
