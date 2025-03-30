namespace codecrafters_http_server
{
    public class HttpResponseBody
    {
        public string ResponseMessage { get; set; }

        public string StatusCode { get; set; }

        public string ContentType { get; set; }

        public long SizeInBytes { get; set; }

        public HttpResponseBody(string responseMessage, string statusCode, string contentType, long sizeInBytes)
        {
            ResponseMessage = responseMessage;

            StatusCode = statusCode;

            ContentType = contentType;

            SizeInBytes = sizeInBytes;

        }
    }
}