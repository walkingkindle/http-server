using codecrafters_http_server.src.Application.Interfaces;
using codecrafters_http_server.src.Application.Services.Helpers;
using codecrafters_http_server.src.Domain.Entities;
using HttpMethod = codecrafters_http_server.src.Domain.Entities.HttpMethod;


namespace codecrafters_http_server.src.Application.Services.Routes.POST
{
    public class FilesRoute : IHttpRouteHandler
    {
        public override string _route { get; set; } = "/files";
        public override HttpMethod _method { get; set; } = HttpMethod.Create(HttpMethod.POST).Value;


        public override HttpResponse HandleRoute(HttpRequest request)
        {

            var filePath = $"{DirectoryHelpers.GetDirectoryPath(request.Arguments)}{request.Endpoint.Query}";

            File.WriteAllText(filePath, request.Body);

            if (File.Exists(filePath))
            {
                Console.WriteLine("File exists");
            }

            return new HttpResponse("",HttpStatusCodes.GetHttpResponseStatus(HttpStatusCodes.Created), "", 0);


        }
    }
}
