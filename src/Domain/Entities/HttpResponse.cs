namespace codecrafters_http_server.src.Domain.Entities
{
    public class HttpResponse
    {
        public string ResponseMessage { get; set; }

        public string StatusCode { get; set; }

        public string ContentType { get; set; }

        public long SizeInBytes { get; set; }

        public string ContentEncoding { get; set; }

        public HttpResponse(string responseMessage, string statusCode, string contentType, long sizeInBytes)
        {
            ResponseMessage = responseMessage;

            StatusCode = statusCode;

            ContentType = contentType;

            SizeInBytes = sizeInBytes;

        }
    }
}