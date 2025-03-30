namespace codecrafters_http_server.Routes.POST
{
    public class FilesRoute : IHttpRouteHandler
    {
        public override string _endpoint { get; set; } = "/files";
        public override string _method { get; set; } = HttpMethod.POST;


        public override HttpResponse HandleRoute(HttpRequest request)
        {

            string fileName = request.Endpoint.Replace($"{_endpoint}/", "").Trim();

            var filePath = $"{DirectoryHelpers.GetDirectoryPath(request.Arguments)}{fileName}";

            FileStream fs = File.Create(filePath);

            File.WriteAllText(filePath, request.Body);

            if (File.Exists(filePath))
            {
                Console.WriteLine("File exists");
            }

            return new HttpResponse("", HttpStatusCodes.Created, "", 0);


        }
    }
}
