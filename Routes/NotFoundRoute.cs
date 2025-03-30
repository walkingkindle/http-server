namespace codecrafters_http_server.Routes
{
    class NotFoundRoute : IHttpRouteHandler
    {
        public override string _endpoint { get; set; } = null;

        public override HttpResponse HandleRoute(HttpRequest request)
        {
            return new HttpResponse ("", HttpStatusCodes.NotFound, HTTPContentType.TextType,0);
        }
    }
}
