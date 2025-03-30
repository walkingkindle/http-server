using System.Text;

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
                string message = msgSubstring.Replace("echo", " ").Replace('/', ' ').Trim();
                return new HttpResponseBody(message,"200 OK", "text/plain", Encoding.UTF8.GetByteCount(message));
            }

            if (msgSubstring.Trim() == "/user-agent")
            {
                return new HttpResponseBody(request.UserAgent, "200 OK", "text/plain", Encoding.UTF8.GetByteCount(request.UserAgent));
            }

            if (msgSubstring.Trim() == "/")
            {
                return new HttpResponseBody("", "200 OK", "text/plain", Encoding.UTF8.GetByteCount(""));
            }

            if(msgSubstring.Trim().StartsWith("/files"))
            {
                string fileName = msgSubstring.Replace("/files/", " ").Trim();
                DirectoryInfo di = new DirectoryInfo(Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory,@"..\..\..\tmp"));
                FileInfo[] fi = di.GetFiles();

                if (fi.Any(p=> p.Name == fileName))
                {
                    return new HttpResponseBody("", "200 OK", "application/octetstream", fi.FirstOrDefault(p => p.Name == fileName).Length);
                }
            }

            return new HttpResponseBody("", "404 Not Found","text/plain", Encoding.UTF8.GetByteCount(""));

   
            
        }

        public static HttpRequest GetRequestInfo(string msg)
        {
            var msgArray = msg.Split(Environment.NewLine);

            HttpRequest request = new HttpRequest();

            string endpoint = msgArray[0].Substring(msgArray[0].IndexOf("/"), msgArray[0].LastIndexOf("HTTP") - msgArray[0].IndexOf("/"));

            request.Endpoint = endpoint;

            Dictionary<string, string> dict = new();

            for(int i = 1; i < msgArray.Length; i++)
            {
              if (msgArray[i].Contains("Accept")){
                  request.Accept = msgArray[i].Replace("Accept", " ").Replace(": "," ").Trim();
               }

              else if (msgArray[i].Contains("User-Agent")){
                 request.UserAgent = msgArray[i].Replace("User-Agent", " ").Replace(": "," ").Trim();
              }

              else if (msgArray[i].Contains("Host")){
                 request.Host = msgArray[i].Replace("Host", " ").Replace(": "," ").Trim();
              }
        
            }

            return request;

        }
    }
}
