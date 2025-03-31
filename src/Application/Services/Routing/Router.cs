using codecrafters_http_server.src.Application.Interfaces;
using codecrafters_http_server.src.Domain.Entities;

namespace codecrafters_http_server.src.Application.Services.Routing
{
    public class Router
    {
        private readonly IRouteManager _routeManager;
        public Router()
        {
            _routeManager = new RouterManager();
        }
        public HttpResponse DelegateToRouter(HttpRequest request)
        {
            var handler = _routeManager.GetHandler(request);

            return handler.HandleRoute(request);
        }
    }
}
