namespace codecrafters_http_server
{
    public class HttpResponseBody
    {
        public string ResponseMessage { get; set; }

        public string StatusCode { get; set; }

        public HttpResponseBody(string responseMessage, string statusCode)
        {
            ResponseMessage = responseMessage;

            StatusCode = statusCode;

        }
    }
}