namespace codecrafters_http_server
{
    public class HttpRequest
    {
        public  string Endpoint { get; set; }
        public  string Host { get; set; }

        public  string UserAgent { get; set; }

        public  string Accept { get; set; }

        public string?[] Arguments { get; set; }

    }
}
