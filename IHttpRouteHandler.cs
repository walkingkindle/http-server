namespace codecrafters_http_server
{
    public abstract class  IHttpRouteHandler
    {
        public abstract string _endpoint { get; set; }
        public abstract string _method { get; set; }
        public abstract HttpResponse HandleRoute(HttpRequest request);
  
    }
}
