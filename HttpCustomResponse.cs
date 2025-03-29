namespace codecrafters_http_server
{
    public class HttpCustomResponse
    {
        public string ResponseMessage { get; set; }

        public string StatusCode { get; set; }

        public HttpCustomResponse(string responseMessage, string statusCode)
        {
            ResponseMessage = responseMessage;

            StatusCode = statusCode;

        }
    }
}