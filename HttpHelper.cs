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

            if (msgSubstring.Trim() == "/user-agent")
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

            string endpoint = msgArray[0].Substring(msg.IndexOf("/"), msg.LastIndexOf("HTTP") - msg.IndexOf("/"));

            Dictionary<string, string> dict = new();

            for(int i = 1; i < msgArray.Length; i++)
            {
              if (msgArray[i].Contains("Accept")){
                dict.Add("Accept", msgArray[i].Replace("Accept", " ").Trim());
               }

              else if (msgArray[i].Contains("User-Agent")){
                dict.Add("User-Agent", msgArray[i].Replace("User-Agent", " ").Trim());
              }

              else if (msgArray[i].Contains("Host")){
                dict.Add("Host", msgArray[i].Replace("Host", " ").Trim());
              }
        
            }

            return new HttpRequest
            {
                Endpoint = endpoint,
                Accept = dict["Accept"].Replace(": ", " ").Trim() ?? "",
                UserAgent = dict["User-Agent"].Replace(": ", " ").Trim() ?? "",
                Host = dict["Host"].Replace(": ", " ").Trim() ?? "" 
            };

        }
    }
}
