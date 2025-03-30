namespace codecrafters_http_server.Routes.POST
{
    public class FilesRoute : IHttpRouteHandler
    {
        public override string _endpoint { get; set; } = "/files";
        public override string _method { get; set; } = HttpMethod.POST;


        public override HttpResponse HandleRoute(HttpRequest request)
        {
            string fileName = request.Endpoint.Replace(_endpoint, "");
            var filePath = $"{DirectoryHelpers.GetDirectoryPath(request.Arguments)}{fileName.Replace("/","")}";

            FileStream fs = File.Create(filePath);

            return new HttpResponse("", HttpStatusCodes.Created, "", 0);


        }
    }
}
