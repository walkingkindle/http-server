namespace codecrafters_http_server
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
