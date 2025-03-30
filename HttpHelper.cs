using System.Text;

namespace codecrafters_http_server
{
    public class HttpHelper
    {
      
        

        //public HttpResponseBody GetMessageToResend(string msg, HttpRequest request, string filePath = null)
        //{
        //    var from = msg.IndexOf("/");
        //    var to = msg.LastIndexOf("HTTP");
        //    string msgSubstring = msg.Substring(from, to - from);

        //    if (msgSubstring.Trim() == "/echo")
        //    {
        //        string message = msgSubstring.Replace("echo", " ").Replace('/', ' ').Trim();
        //        return new HttpResponseBody(message,"200 OK", "text/plain", Encoding.UTF8.GetByteCount(message));
        //    }

        //    if (msgSubstring.Trim() == "/user-agent")
        //    {
        //        return new HttpResponseBody(request.UserAgent, "200 OK", "text/plain", Encoding.UTF8.GetByteCount(request.UserAgent));
        //    }

        //    if (msgSubstring.Trim() == "/")
        //    {
        //        return new HttpResponseBody("", "200 OK", "text/plain", Encoding.UTF8.GetByteCount(""));
        //    }

        //    if(msgSubstring.Trim().StartsWith("/files"))
        //    {
        //        string fileName = msgSubstring.Replace("/files/", " ").Trim();
        //        var filePath = $"{filepath}/{fileName}";

        //        if (File.Exists(filePath))
        //        {
        //            return new HttpResponseBody(File.ReadAllText(filePath), "200 OK", "application/octet-stream",File.ReadAllBytes(filePath).LongLength );
        //        }
        //    }

        //    return new HttpResponseBody("", "404 Not Found","text/plain", Encoding.UTF8.GetByteCount(""));

   
            
        //}


        public static HttpRequest GetRequestInfo(string msg, string[] args)
        {
            var msgArray = msg.Split(Environment.NewLine);

            HttpRequest request = new HttpRequest();

            string endpoint = msgArray[0].Substring(msgArray[0].IndexOf("/"), msgArray[0].LastIndexOf("HTTP") - msgArray[0].IndexOf("/"));

            request.Endpoint = endpoint;

            Dictionary<string, string> dict = new();

            for(int i = 1; i < msgArray.Length; i++)
            {
              if (msgArray[i].Contains("Accept")){
                  request.Accept = msgArray[i].Replace("Accept", "").Replace(": ","").Trim();
               }

              else if (msgArray[i].Contains("User-Agent")){
                 request.UserAgent = msgArray[i].Replace("User-Agent", "").Replace(": ","").Trim();
              }

              else if (msgArray[i].Contains("Host")){
                 request.Host = msgArray[i].Replace("Host", "").Replace(": ","").Trim();
              }
              else if(args.Length > 0)
                {
                    request.Arguments = args;
                }
        
            }

            return request;

        }

        public static string BuildHeaders(HttpResponse response)
        {
            return $"{response.ResponseMessage}, {response.StatusCode}, {response.ContentType}, {response.SizeInBytes}"; 
        }
    }
}
