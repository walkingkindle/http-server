namespace codecrafters_http_server
{
    public static class HttpHelper
    {

        public static HttpResponseBody GetMessageToResend(string msg, HttpRequest request)
        {
            var from = msg.IndexOf("/");
            var to = msg.LastIndexOf("HTTP");
            string msgSubstring = msg.Substring(from, to - from);

            if (msg.Contains("echo"))
            {
                return new HttpResponseBody(msgSubstring.Replace("echo", " ").Replace('/', ' ').Trim(), "200 OK");
            }

            if (msg.Contains("user-agent"))
            {
                return new HttpResponseBody(request.UserAgent, "200 OK");
            }

            if (msgSubstring.Trim() == "/")
            {
                return new HttpResponseBody("", "200 OK");
            }

            return new HttpResponseBody("", "404 Not Found");

   
            
        }

        public static HttpRequest GetRequestInfo(string msg)
        {
            var msgArray = msg.Split(':','H','U','A');

            return new HttpRequest
            {
                Accept = msgArray[9].Trim(),
                Endpoint = msgArray[0].Replace("GET", " ").Trim(),
                Host = msgArray[3].Trim(),
                UserAgent = msgArray[7].Trim()
            };
        }
    }
}
