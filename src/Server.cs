using System.Net;
using System.Net.Sockets;
using System.Text;
using codecrafters_http_server;
using CSharpFunctionalExtensions;

internal class Program
{
    private static async Task Main(string[] args)
    {
        var ipEndPoint = new IPEndPoint(IPAddress.Any, 4221);
        TcpListener listener = new(ipEndPoint);

        listener.Start();
        RouterManager routerManager = new RouterManager();
               Console.WriteLine("Server is running on port 4221...");

        try
        {
            while (true)
            {
                TcpClient handler = await listener.AcceptTcpClientAsync();
                _ = Task.Run(() => HandleClient(handler, args, routerManager));
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

        async Task HandleClient(TcpClient handler, string[] args, RouterManager routerManager)
        {
            try
            {
                await using NetworkStream stream = handler.GetStream();

                NetworkStreamReader reader = new NetworkStreamReader(handler, stream, args);

                Result<HttpRequest> request = await reader.GetHttpRequestFromBuffer();

                if (request.IsFailure)
                {
                    throw new Exception(request.Error);
                }

                HttpResponse? response = routerManager.GetHandler(request.Value)?.HandleRoute(request.Value);

                await stream.WriteAsync(Encoding.UTF8.GetBytes(HttpHelper.BuildHeaders(response)));
                await stream.WriteAsync(Encoding.UTF8.GetBytes(response.ResponseMessage));
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