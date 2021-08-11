using System;
using System.Net.Sockets;
using System.Net;
using System.IO;
using System.Threading;
using System.Text;


namespace SocketTest.Client
{
    public class ShellClient
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

                string command = "";
                while (!command.Contains("quit"))
                {
                    Log.WriteSingle("localhost@", ConsoleColor.DarkYellow);
                    Log.WriteSingle(IP + ":", ConsoleColor.Yellow);
                    command = Console.ReadLine();

                    byte[] data = Encoding.ASCII.GetBytes(command);

                    try
                    {
                        NetworkStream stream = client.GetStream();
                        stream.Write(data, 0, data.Length);
                        Log.Success("Sent command to network.");
                        // Buffer to store the response bytes.
                        data = new Byte[256];
                        // String to store the response ASCII representation.
                        String responseData = String.Empty;

                        // Read the first batch of the TcpServer response bytes.
                        Int32 bytes = stream.Read(data, 0, data.Length);
                        responseData = System.Text.Encoding.ASCII.GetString(data, 0, bytes);
                        Log.Write("Server Says: " + responseData, ConsoleColor.DarkYellow);
                    }
                    catch (IOException e)
                    {
                        Log.Error(e.Message);
                    }
                }
            }
            catch (SocketException e)
            {
                Log.Error(e.Message);
            }

            Log.Write("Client exiting...");
            Console.ReadKey();
        }
    }
}