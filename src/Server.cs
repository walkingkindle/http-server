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
    Console.WriteLine(msg);
    var messageArray = msg.ToCharArray();
    string messageToReturn = "";
    for(int i = 0; i < messageArray.Length; i++)
    {
        if (messageArray[i] == '/')
        {
            messageToReturn = messageArray[i + 1] == ' ' ? "HTTP/1.1 200 OK\r\n\r\n" : "HTTP/1.1 404 NOT FOUND\r\n\r\n";
            break;

        }

    }
    var dateTimeBytes = Encoding.UTF8.GetBytes(messageToReturn);
    await stream.WriteAsync(dateTimeBytes);

    Console.WriteLine($"Sent message: \"{messageToReturn}\"");

}
finally
{
    listener.Stop();
}
