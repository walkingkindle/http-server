using codecrafters_http_server.src.Domain.Entities;
using HttpMethod = codecrafters_http_server.src.Domain.Entities.HttpMethod;
namespace codecrafters_http_server.src.Application.Interfaces
{
    public abstract class  IHttpRouteHandler
    {
        public abstract string _route { get; set; }
        public abstract HttpMethod _method { get; set; }
        public abstract HttpResponse HandleRoute(HttpRequest request);
  
    }
}
