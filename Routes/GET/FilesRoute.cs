namespace codecrafters_http_server.Routes.GET
{
    public class FilesRoute : IHttpRouteHandler
    {
        public override string _endpoint { get; set; } = "/files";

        public override string _method { get; set; } = HttpMethod.GET;
        public override HttpResponse HandleRoute(HttpRequest request)
        {
            string fileName = request.Endpoint.Replace("/files/", "").Trim();
            var filePath = $"{DirectoryHelpers.GetDirectoryPath(request.Arguments)}{fileName}";

            if (File.Exists(filePath))
            {
                return new HttpResponse(File.ReadAllText(filePath), HttpStatusCodes.OK, HTTPContentType.FileType,File.ReadAllBytes(filePath).LongLength );
            }

            return new HttpResponse("", HttpStatusCodes.GetHttpResponseStatus(HttpStatusCodes.NotFound), HTTPContentType.FileType, 0);
        }

    
    }
}
