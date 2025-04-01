using codecrafters_http_server.src.Application.Interfaces;
using codecrafters_http_server.src.Application.Services.Routes.GET;
using codecrafters_http_server.src.Domain.Entities;
using System.Collections.Generic;
using HttpMethod = codecrafters_http_server.src.Domain.Entities.HttpMethod;

namespace codecrafters_http_server.src.Application.Services.Routing
{
    public class RouteManagerService : IRouteManagerService
    {
        private readonly IEnumerable<IHttpRouteHandler> _routeHandlers;

        public RouteManagerService(IEnumerable<IHttpRouteHandler> routeHandlers)
        {
            _routeHandlers = routeHandlers;
        }
        public IHttpRouteHandler? GetHandler(Endpoint requestEndpoint, HttpMethod requestMethod)
        {
            IHttpRouteHandler route =  _routeHandlers.FirstOrDefault(p => requestEndpoint.Route == p._route && requestMethod.Equals(p._method));

            return route ?? new NotFoundRouteHandler();

         }
    }
}
