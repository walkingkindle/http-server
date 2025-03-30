namespace codecrafters_http_server
{
    public interface IRouteManager
    {
        public IHttpRouteHandler? GetHandler(HttpRequest request);
    }
}
