using codecrafters_http_server.src.Domain.Entities;
using CSharpFunctionalExtensions;
using System.Net.Sockets;
using System.Text;
using HttpMethod = codecrafters_http_server.src.Domain.Entities.HttpMethod;


namespace codecrafters_http_server.src.Infrastructure.Networking
{
    public class NetworkStreamReader
    {
        private readonly TcpClient _handler;
        private readonly NetworkStream _stream;
        private readonly string[] _args;
        public NetworkStreamReader(TcpClient handler, NetworkStream stream, string[] args)
        {
            _handler = handler;
            _stream = stream;
            _args = args;
        }

        public async Task<Result<HttpRequest>> GetHttpRequestFromBuffer()
        {
            Result<string> bufferString = await GetMessageStringFromBuffer(_handler, _stream);

            if (bufferString.IsFailure)
            {
                return Result.Failure<HttpRequest>(bufferString.Error);
            }
            return GetHttpRequestFromStringBuffer(bufferString.Value);
              
        }

        private async Task<Result<string>> GetMessageStringFromBuffer(TcpClient handler, NetworkStream stream)
        {
            byte[] buffer = new byte[handler.ReceiveBufferSize];
            int bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);
            if (bytesRead == 0) return Result.Failure<string>(TcpClientErrors.NullBuffer);
            string msg = Encoding.ASCII.GetString(buffer, 0, bytesRead);

            return Result.Success(msg);
        }

        private Result<HttpRequest> GetHttpRequestFromStringBuffer(string msg)
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
              if (msgArray[i].Contains("Accept")){
                  contentTypeResult = HttpContentType.Create(msgArray[i].Replace("Accept", "").Replace(": ",""));
               }

              else if (msgArray[i].Contains("User-Agent")){
                 request.UserAgent = msgArray[i].Replace("User-Agent", "").Replace(": ","").Trim();
              }

              else if (msgArray[i].Contains("Host")){
                 request.Host = msgArray[i].Replace("Host", "").Replace(": ","").Trim();
              }
            }

             if(_args.Length > 0)
             {
                request.Arguments = _args;
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
