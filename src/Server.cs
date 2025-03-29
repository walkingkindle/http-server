using codecrafters_http_server;
using System.Net;
using System.Net.Sockets;
using System.Text;

var ipEndPoint = new IPEndPoint(IPAddress.Any, 4221);
TcpListener listener = new(ipEndPoint);

  
listener.Start();


try
{
    using TcpClient handler = await listener.AcceptTcpClientAsync();

    await using NetworkStream stream = handler.GetStream();

    byte[] buffer = Array.Empty<byte>();

    buffer = new byte[handler.ReceiveBufferSize];
    int bytesRead = await stream.ReadAsync(buffer, 0, handler.ReceiveBufferSize);
    string msg = Encoding.ASCII.GetString(buffer,0, bytesRead);
    string messageToResend = HttpClientTestCaller.GetMessageToResend(msg);
    Console.WriteLine(messageToResend);
    string responseHeaders = $"HTTP/1.1 200 OK\r\nContent-Type: text/plain\r\nContent-Length: {Encoding.UTF8.GetByteCount(messageToResend)}\r\n\r\n";
    await stream.WriteAsync(Encoding.UTF8.GetBytes(responseHeaders));
    await stream.WriteAsync(Encoding.UTF8.GetBytes(messageToResend));

}
finally
{
    listener.Stop();
}
