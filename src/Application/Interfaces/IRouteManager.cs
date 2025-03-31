using codecrafters_http_server.src.Domain.Entities;

namespace codecrafters_http_server.src.Application.Interfaces
{
    public interface IRouteManager
    {
        public IHttpRouteHandler? GetHandler(HttpRequest request);
    }
}
