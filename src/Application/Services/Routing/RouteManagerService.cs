using codecrafters_http_server.src.Application.Interfaces;
using codecrafters_http_server.src.Application.Services.Routes.GET;
using codecrafters_http_server.src.Domain.Entities;
using System.Collections.Generic;

namespace codecrafters_http_server.src.Application.Services.Routing
{
    public class RouteManagerService : IRouteManagerService
    {
        private readonly IEnumerable<IHttpRouteHandler> _routeHandlers;

        public RouteManagerService(IEnumerable<IHttpRouteHandler> routeHandlers)
        {
            _routeHandlers = routeHandlers;
        }
        public IHttpRouteHandler? GetHandler(HttpRequest request)
        {
            IHttpRouteHandler route =  _routeHandlers.FirstOrDefault(p => request.Endpoint.Route == p._route && request.Method.Equals(p._method));

            return route ?? new NotFoundRouteHandler();

         }
    }
}
