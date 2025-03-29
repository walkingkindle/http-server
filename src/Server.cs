using codecrafters_http_server;
using System.Net;
using System.Net.Sockets;
using System.Text;

var ipEndPoint = new IPEndPoint(IPAddress.Any, 4221);
TcpListener listener = new(ipEndPoint);

  
listener.Start();


while (true)
{
    using TcpClient handler = await listener.AcceptTcpClientAsync();

    await using NetworkStream stream = handler.GetStream();
   
    var message = "HTTP/1.1 200 OK";
    var dateTimeBytes = Encoding.UTF8.GetBytes(message);
    await stream.WriteAsync(dateTimeBytes);

    Console.WriteLine($"Sent message: \"{message}\"");
}
