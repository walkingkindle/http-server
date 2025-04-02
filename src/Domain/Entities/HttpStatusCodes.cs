namespace codecrafters_http_server.src.Domain.Entities
{
    public  class HttpStatusCode
    {
        public static readonly string OK = "200 OK";

        public static readonly string Created = "201 Created";

        public static readonly string NotFound = "404 Not Found";

        public static readonly string InternalServerError = "500 Internal Server Error";

        private readonly string _value;

        public string Value { get; set; }
        public HttpStatusCode(string value)
        {
            Value = GetHttpResponseStatus(value);
        }

        public static string GetHttpResponseStatus(string status)
        {
            return "HTTP/1.1 " + status;
        }
    }


}
