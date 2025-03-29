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
    stream.Read(buffer, 0, handler.ReceiveBufferSize);
    string msg = Encoding.ASCII.GetString(buffer);
    string messageToResend = HttpClientTestCaller.GetMessageToResend(msg);
    Console.WriteLine(messageToResend);
    var messageToSend = Encoding.UTF8.GetBytes($"HTTP/1.1 200 OK\r\nContent-Type: text/plain\r\nContent-Length: 3\r\n\r\n{messageToResend}");
    await stream.WriteAsync(messageToSend);

}
finally
{
    listener.Stop();
}
