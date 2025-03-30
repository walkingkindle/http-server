
using codecrafters_http_server.Routes;

namespace codecrafters_http_server
{
    public class RouterManager : IRouteManager
    {
        private readonly List<IHttpRouteHandler> RouteHandlers;

        public RouterManager()
        {
            RouteHandlers = new List<IHttpRouteHandler> { new DefaultRoute(), new UserAgentRoute(), new EchoRoute(), new FilesRoute(), new NotFoundRoute() };
        }
        public IHttpRouteHandler? GetHandler(HttpRequest request)
        {
            IHttpRouteHandler route =  RouteHandlers.FirstOrDefault(p => ExtractEndpoint(request.Endpoint)== p._endpoint);

            if(route is null)
            {
                return new NotFoundRoute();
            }
        }

        private string ExtractEndpoint(string wholeEndpoint)
        {

            var from = wholeEndpoint.IndexOf("/");
            var to = wholeEndpoint.LastIndexOf("/");
            string msgSubstring = wholeEndpoint.Substring(from, to - from);

            return string.IsNullOrEmpty(msgSubstring) ? wholeEndpoint.Trim() : msgSubstring.Trim();

        }
    }
}
