using codecrafters_http_server.src.Application.Interfaces;
using codecrafters_http_server.src.Application.Services.Helpers;
using codecrafters_http_server.src.Domain.Entities;
using System.Text;
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
                byte[] responseBody = Encoding.UTF8.GetBytes(File.ReadAllText(filePath));
                return new HttpResponse(new HttpStatusCode(HttpStatusCode.OK), HttpContentType.FileType,null, new HttpResponseBody(responseBody, responseBody.Length));
            }

            return new HttpResponse(new HttpStatusCode(HttpStatusCode.NotFound), HttpContentType.FileType);
        }

    
    }
}
