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
            var msgArray = msg.Split(Environment.NewLine);

            string[] keywords = ["Host:", "Accept:", "User-Agent:"];

            string endpoint = msg.Substring(msg.IndexOf("/"), msg.LastIndexOf("HTTP") - msg.IndexOf("/"));

            Dictionary<string, string> dict = new();

            for(int i = 0; i < msgArray.Length; i++)
            {
                for(int a = 0; a < keywords.Length; a++)
                {
                  if (msgArray[i].Contains(keywords[a]))
                   {
                    dict.Add(keywords[a].Replace(':', ' ').Trim(), msgArray[i].Replace(keywords[a], " ").Trim());
                   }

                }
        
            }

            return new HttpRequest {
                Endpoint = endpoint,
                Accept = dict["Accept"],
                UserAgent = dict["User-Agent"],
                Host = dict["Host"]
            };

        }
    }
}
