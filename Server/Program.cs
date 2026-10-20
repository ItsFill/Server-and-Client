using System.Net;
using System.Net.Sockets;
using System.Text;

class Program
{
    static async Task Main(string[] args)
    {
        var ipEndPoint = new IPEndPoint(IPAddress.Any, 8888);
        TcpListener listener = new TcpListener(ipEndPoint);
        try
        {
            listener.Start();
            using TcpClient client = await listener.AcceptTcpClientAsync();
            await using var stream = client.GetStream();
            //FC - From Client
            byte[] bufferFC = new byte[1024];
            int bytesReadFC = await stream.ReadAsync(bufferFC, 0, bufferFC.Length);
            string messageFC = Encoding.UTF8.GetString(bufferFC, 0, bytesReadFC);
            
            Console.WriteLine("Client: " + messageFC + "\n");
            
            //TC - To Client
            string messageTC = "Hello Client!";
            byte[] bufferTC = Encoding.UTF8.GetBytes(messageTC);
            Console.WriteLine("Server: " + messageTC + "\n");
            
            await stream.WriteAsync(bufferTC, 0, bufferTC.Length);




        }
        finally
        {
            listener.Stop();
        }
    }
}