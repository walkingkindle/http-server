
using codecrafters_http_server.Routes;
using codecrafters_http_server.Routes.GET;

namespace codecrafters_http_server
{
    public class RouterManager : IRouteManager
    {
        private readonly List<IHttpRouteHandler> RouteHandlers;

        public RouterManager()
        {
            RouteHandlers = new List<IHttpRouteHandler> { new DefaultRoute(), new UserAgentRoute(), new EchoRoute(), new FilesRoute(), new NotFoundRoute(), new Routes.POST.FilesRoute() };
        }
        public IHttpRouteHandler? GetHandler(HttpRequest request)
        {
            IHttpRouteHandler route =  RouteHandlers.FirstOrDefault(p => request.Endpoint.Route == p._route && request.Method.Equals(p._method));

            if(route is null)
            {
                return new NotFoundRoute();
            }
            return route;
        }
    }
}
