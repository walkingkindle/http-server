
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
            string extractedEndpoint = ExtractEndpoint(request.Endpoint);
            IHttpRouteHandler route =  RouteHandlers.FirstOrDefault(p => extractedEndpoint == p._endpoint && request.Method == p._method);

            if(route is null)
            {
                return new NotFoundRoute();
            }
            return route;
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
