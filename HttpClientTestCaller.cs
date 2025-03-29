namespace codecrafters_http_server
{
    public static class HttpClientTestCaller
    {

        public static HttpCustomResponse GetMessageToResend(string msg)
        {
            var from = msg.IndexOf("/");
            var to = msg.LastIndexOf("HTTP");
            string msgSubstring = msg.Substring(from, to - from);

            if (msg.Contains("echo"))
            {
                return new HttpCustomResponse(msgSubstring.Replace("echo", " ").Replace('/', ' ').Trim(), "200 OK");
            }

            if (msgSubstring.Trim() == "/")
            {
                return new HttpCustomResponse("", "200 OK");
            }

            return new HttpCustomResponse("", "404 Not Found");

   
            
        }
    }
}
