using codecrafters_http_server.src.Domain.Entities;

namespace codecrafters_http_server.src.Application.Interfaces
{
    public interface IRouteManagerService
    {
        public IHttpRouteHandler? GetHandler(HttpRequest request);
    }
}
