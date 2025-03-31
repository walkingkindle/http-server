namespace codecrafters_http_server
{
    public abstract class  IHttpRouteHandler
    {
        public abstract string _route { get; set; }
        public abstract HttpMethod _method { get; set; }
        public abstract HttpResponse HandleRoute(HttpRequest request);
  
    }
}
