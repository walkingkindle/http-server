
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
            return RouteHandlers.FirstOrDefault(p => ExtractEndpoint(request.Endpoint)== p._endpoint);
        }

        private string ExtractEndpoint(string wholeEndpoint)
        {

            if(wholeEndpoint.Trim() == "/")
            {
                return "/";
            }

            var from = wholeEndpoint.IndexOf("/");
            var to = wholeEndpoint.LastIndexOf("/");
            string msgSubstring = wholeEndpoint.Substring(from, to - from);

            return msgSubstring;

        }
    }
}
