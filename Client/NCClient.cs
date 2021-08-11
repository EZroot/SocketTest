using System;
using System.Net.Sockets;
using System.Net;
using System.IO;
using System.Threading;
using System.Text;

namespace SocketTest.Client
{
    public class NCClient
    {
        public static string IP = "127.0.0.1";
        public static int Port = 1337;

        public static void Start()
        {
            Log.Warn("Trying to connect to " + IP);
            try
            {
                TcpClient client = new TcpClient(IP, Port);
                Log.Success("Connected!");

                Byte[] data;

                while (true)
                {
                    // Buffer to store the response bytes.
                    data = new Byte[256];

                    // String to store the response ASCII representation.
                    String responseData = String.Empty;

                    NetworkStream stream = client.GetStream();

                    // Read the first batch of the TcpServer response bytes.
                    Int32 bytes = stream.Read(data, 0, data.Length);
                    responseData = System.Text.Encoding.ASCII.GetString(data, 0, bytes);
                    Log.Write("Server Says: " + responseData, ConsoleColor.DarkYellow);
                }
            }
            catch (SocketException e)
            {
                Log.Error(e.Message);
            }
            Log.Warn("Client Exited. Press any key to continue.");
            Console.ReadKey();
        }
    }
}