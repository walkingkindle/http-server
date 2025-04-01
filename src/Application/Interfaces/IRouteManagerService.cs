using codecrafters_http_server.src.Domain.Entities;
using HttpMethod = codecrafters_http_server.src.Domain.Entities.HttpMethod;

namespace codecrafters_http_server.src.Application.Interfaces
{
    public interface IRouteManagerService
    {
        public IHttpRouteHandler? GetHandler(Endpoint requestEndpoint, HttpMethod requestMethod);
    }
}
