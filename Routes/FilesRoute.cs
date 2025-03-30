namespace codecrafters_http_server.Routes
{
    public class FilesRoute : IHttpRouteHandler
    {
        public override string _endpoint { get; set; } = "/files";

        public override HttpResponse HandleRoute(HttpRequest request)
        {
            string fileName = request.Endpoint.Replace("/files/", "").Trim();
            var filePath = $"{GetDirectoryPath(request.Arguments)}{fileName}";

            if (File.Exists(filePath))
            {
                return new HttpResponse(File.ReadAllText(filePath), HttpStatusCodes.OK, HTTPContentType.FileType,File.ReadAllBytes(filePath).LongLength );
            }

            return new HttpResponse("", HttpStatusCodes.NotFound, HTTPContentType.FileType, 0);
        }

     private static string GetDirectoryPath(string[] args)
        {
        string directoryPath = null;

        for (int i = 0; i < args.Length; i++)
        {
            if (args[i] == "--directory" && i + 1 < args.Length)
            {
                directoryPath = args[i + 1];
                break;
            }
        }
        return directoryPath;
    }
    }
}
