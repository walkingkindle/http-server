namespace codecrafters_http_server.Routes.GET
{
    class NotFoundRoute : IHttpRouteHandler
    {
        public override string _endpoint { get; set; } = null;

        public override string _method { get; set; } = HttpMethod.GET;


        public override HttpResponse HandleRoute(HttpRequest request)
        {
            return new HttpResponse ("", HttpStatusCodes.NotFound, HTTPContentType.TextType,0);
        }
    }
}
