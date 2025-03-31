﻿namespace codecrafters_http_server
{
    public class HttpRequest
    {
        public HttpMethod Method { get; set; }
        public string? Body { get; set; }
        public Endpoint Endpoint { get; set; }
        public string Host { get; set; }
        public string UserAgent { get; set; }
        public HttpContentType Accept { get; set; }
        public string?[] Arguments { get; set; }

    }
}
