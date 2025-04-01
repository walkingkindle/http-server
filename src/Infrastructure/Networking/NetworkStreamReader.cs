using codecrafters_http_server.src.Domain.Entities;
using codecrafters_http_server.src.Infrastructure.Interfaces;
using CSharpFunctionalExtensions;
using System.Net.Sockets;
using System.Text;
using HttpMethod = codecrafters_http_server.src.Domain.Entities.HttpMethod;


namespace codecrafters_http_server.src.Infrastructure.Networking
{
    public class NetworkStreamReader:INetworkStreamReader
    {
        public NetworkStreamReader()
        {
        }

        public async Task<Result<HttpRequest>> GetHttpRequestFromBuffer(string[] args, NetworkStream stream, TcpClient handler)
        {
            Result<string> bufferString = await GetMessageStringFromBuffer(stream, handler);

            if (bufferString.IsFailure)
            {
                return Result.Failure<HttpRequest>(bufferString.Error);
            }
            return GetHttpRequestFromStringBuffer(bufferString.Value, args);
              
        }

        private async Task<Result<string>> GetMessageStringFromBuffer(NetworkStream stream, TcpClient handler)
        {
            byte[] buffer = new byte[handler.ReceiveBufferSize];
            int bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);
            if (bytesRead == 0) return Result.Failure<string>("Did not read anything from the buffer.");
            string msg = Encoding.ASCII.GetString(buffer, 0, bytesRead);

            return Result.Success(msg);
        }

        private Result<HttpRequest> GetHttpRequestFromStringBuffer(string msg, string[] args)
        {
             var msgArray = msg.Split(Environment.NewLine);

            HttpRequest request = new HttpRequest();

            Result<Endpoint> endpointResult = Endpoint.Create(msgArray[0].Substring(msgArray[0].IndexOf("/"), msgArray[0].LastIndexOf("HTTP") - msgArray[0].IndexOf("/")));

            Result<HttpMethod> httpMethodResult = HttpMethod.Create(msgArray[0].Substring(0, msgArray[0].IndexOf("/", StringComparison.Ordinal)));

            int headerEndIndex = msg.IndexOf("\r\n\r\n");

            request.Body = msg.Substring(headerEndIndex + 4) ?? null;

            Result<HttpContentType> contentTypeResult = new Result<HttpContentType>();

            for(int i = 1; i < msgArray.Length; i++)
            {
              if (msgArray[i].Contains("Accept:")){
                  contentTypeResult = HttpContentType.Create(msgArray[i].Replace("Accept: ", "").Trim());
               }

              else if (msgArray[i].Contains("User-Agent")){
                 request.UserAgent = msgArray[i].Replace("User-Agent", "").Replace(": ","").Trim();
              }

              else if (msgArray[i].Contains("Host")){
                 request.Host = msgArray[i].Replace("Host", "").Replace(": ","").Trim();
              }
              else if (msgArray[i].Contains("Accept-Encoding")){
                 request.AcceptEncoding = msgArray[i].Replace("Accept-Encoding", "").Replace(": ", "").Trim();
                }
            }

             if(args.Length > 0)
             {
                request.Arguments = args;
             }
            Result httpRequestResult = Result.Combine(endpointResult, httpMethodResult, contentTypeResult);

            if (httpMethodResult.IsFailure)
            {
                return Result.Failure<HttpRequest>(httpMethodResult.Error);
            }

            request.Accept = contentTypeResult.Value;
            request.Method = httpMethodResult.Value;
            request.Endpoint = endpointResult.Value;

           

            return request;
        }
    }
}
