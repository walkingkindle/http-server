namespace codecrafters_http_server.src.Domain.Entities
{
    public class HttpResponse
    {
        public HttpStatusCode StatusCode { get; set; }

        public string ContentType { get; set; }

        public string? ContentEncoding { get; set; }

        public HttpResponseBody HttpResponseBody { get; set; }

        public HttpResponse(HttpStatusCode statusCode, string contentType, string contentEncoding=null, HttpResponseBody responseBody = null)
        {

            StatusCode = statusCode;

            ContentType = contentType;

            ContentEncoding = contentEncoding;

            HttpResponseBody = responseBody;

        }
    }
}