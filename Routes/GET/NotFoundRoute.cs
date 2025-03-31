namespace codecrafters_http_server.Routes.GET
{
    class NotFoundRoute : IHttpRouteHandler
    {
        public override string _route { get; set; } = null;

        public override HttpMethod _method { get; set; } = HttpMethod.Create(HttpMethod.GET).Value;


        public override HttpResponse HandleRoute(HttpRequest request)
        {
            return new HttpResponse ("", HttpStatusCodes.GetHttpResponseStatus(HttpStatusCodes.NotFound), HttpContentType.TextType,0);
        }
    }
}
