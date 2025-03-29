using System.Net;
using System.Net.Sockets;
using System.Text;
using codecrafters_http_server;

var ipEndPoint = new IPEndPoint(IPAddress.Any, 4221);
TcpListener listener = new(ipEndPoint);

listener.Start();
Console.WriteLine("Server is running on port 4221...");

try
{
    while (true)  
    {
        TcpClient handler = await listener.AcceptTcpClientAsync(); 
        _ = Task.Run(() => HandleClient(handler)); 
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

async Task HandleClient(TcpClient handler)
{
    try
    {
        await using NetworkStream stream = handler.GetStream();
        
        byte[] buffer = new byte[handler.ReceiveBufferSize];
        int bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);
        if (bytesRead == 0) return;
        string msg = Encoding.ASCII.GetString(buffer, 0, bytesRead);
        HttpRequest request = HttpHelper.GetRequestInfo(msg);
        HttpResponseBody messageToResend = HttpHelper.GetMessageToResend(msg, request);
        Console.WriteLine(messageToResend);

        string responseHeaders = $"HTTP/1.1 {messageToResend.StatusCode}\r\nContent-Type: text/plain\r\nContent-Length: {Encoding.UTF8.GetByteCount(messageToResend.ResponseMessage)}\r\n\r\n";
        
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