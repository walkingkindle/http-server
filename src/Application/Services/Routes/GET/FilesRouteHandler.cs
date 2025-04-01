using codecrafters_http_server.src.Application.Interfaces;
using codecrafters_http_server.src.Application.Services.Helpers;
using codecrafters_http_server.src.Domain.Entities;
using HttpMethod = codecrafters_http_server.src.Domain.Entities.HttpMethod;


namespace codecrafters_http_server.src.Application.Services.Routes.GET
{
    public class FilesRouteHandler : IHttpRouteHandler
    {
        public override string _route { get; set; } = "/files";
        public override HttpMethod _method { get; set; } = HttpMethod.Create(HttpMethod.GET).Value;
        public override HttpResponse HandleRoute(HttpRequest request)
        {
            var filePath = $"{DirectoryHelpers.GetDirectoryPath(request.Arguments)}{request.Endpoint.Query}";

            if (File.Exists(filePath))
            {
                return new HttpResponse(File.ReadAllText(filePath), HttpStatusCodes.GetHttpResponseStatus(HttpStatusCodes.OK), HttpContentType.Create(HttpContentType.FileType).Value.ToString(),File.ReadAllBytes(filePath).LongLength);
            }

            return new HttpResponse("", HttpStatusCodes.GetHttpResponseStatus(HttpStatusCodes.NotFound), HttpContentType.FileType, 0,null);
        }

    
    }
}
