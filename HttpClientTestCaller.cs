namespace codecrafters_http_server
{
    public static class HttpClientTestCaller
    {

        public static string GetMessageToResend(string msg)
        {
            var from = msg.IndexOf("/");
            var to = msg.LastIndexOf("HTTP");
            string messageToSSend = msg.Substring(from, to - from);

            return messageToSSend.Replace('/', ' ').Trim();
        }
    }
}
