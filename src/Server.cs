using System.Net;
using System.Net.Sockets;
using System.Text;
using codecrafters_http_server;

internal class Program
{
    private static async Task Main(string[] args)
    {
        var ipEndPoint = new IPEndPoint(IPAddress.Any, 4221);
        TcpListener listener = new(ipEndPoint);

        listener.Start();
        Console.WriteLine("Server is running on port 4221...");

        try
        {
            while (true)
            {
                TcpClient handler = await listener.AcceptTcpClientAsync();
                if(args == null)
                {
                   _ = Task.Run(() => HandleClient(handler));
                }
                else
                {
                    DirectoryInfo di = new DirectoryInfo(@"C:\Projects\codecraftersproject\http-server\codecrafters-http-server-csharp" + HttpHelper.GetDirectoryPath(args));
                    if (!Directory.Exists(di.FullName))
                    {
                       Directory.CreateDirectory(di.FullName);
                    }
                    _ = Task.Run(() => HandleClient(handler, args));
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Server error: {ex.Message}");
        }
        finally
        {
            listener.Stop();
        }

        async Task HandleClient(TcpClient handler, string[] args=null)
        {
            try
            {
                await using NetworkStream stream = handler.GetStream();

                byte[] buffer = new byte[handler.ReceiveBufferSize];
                int bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);
                if (bytesRead == 0) return;
                string msg = Encoding.ASCII.GetString(buffer, 0, bytesRead);
                HttpRequest request = HttpHelper.GetRequestInfo(msg);
                HttpResponseBody messageToResend = HttpHelper.GetMessageToResend(msg, request, args);
                Console.WriteLine(messageToResend.ResponseMessage);

                string responseHeaders = $"HTTP/1.1 {messageToResend.StatusCode}\r\nContent-Type: {messageToResend.ContentType}\r\nContent-Length: {messageToResend.SizeInBytes}\r\n\r\n";

                await stream.WriteAsync(Encoding.UTF8.GetBytes(responseHeaders));
                await stream.WriteAsync(Encoding.UTF8.GetBytes(messageToResend.ResponseMessage));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Client error: {ex.Message}");
            }
            finally
            {
                handler.Close();
            }
        }
    }
}