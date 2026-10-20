using System.Net;
using System.Net.Sockets;
using System.Text;

class Program
{
    static async Task Main(string[] args)
    {
        var ipEndPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8888);
        TcpClient client = new TcpClient();
        
        await client.ConnectAsync(ipEndPoint);
            
        using NetworkStream stream = client.GetStream();
        
        //TS - To Server
        string messageTS = "Hello Server!";
        byte[] bufferTS = Encoding.UTF8.GetBytes(messageTS);
        await stream.WriteAsync(bufferTS, 0, bufferTS.Length);
        Console.WriteLine("Client: " + messageTS + "\n");
        
        //FS - From Server
        byte[] bufferFS = new byte[1024];
        int bytesRead = await stream.ReadAsync(bufferFS, 0, bufferFS.Length);
        string messageFS = Encoding.UTF8.GetString(bufferFS);
        Console.WriteLine("Server: " + messageFS + "\n");
    }
}