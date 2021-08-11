using System;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Collections.Generic;
using System.Threading;

namespace SocketTest.Server
{
    public class DataServer
    {
        public static string IPString = "127.0.0.1";
        public static int Port = 1337;

        //Might have to make a new client to send ints
        public static void Start()
        {
            IPAddress localIP = IPAddress.Parse(IPString);

            TcpListener server = new TcpListener(localIP, Port);
            server.Start();

            Byte[] bytes = new Byte[256];
            int data = 0;

            while (true)
            {
                Log.Write("Waiting for connection...");
                TcpClient client = server.AcceptTcpClient();
                Log.Success("Connected! ");

                Thread t = new Thread(new ThreadStart(() => ConnectClient(client, bytes, data)));
                t.Start();
            }
            Log.Write("Finished");
        }

        public static void ConnectClient(TcpClient _client, byte[] _bytes, int _data)
        {
            _data = 0;

            NetworkStream stream = _client.GetStream();
            int i;
            while ((i = stream.Read(_bytes, 0, _bytes.Length)) != 0)
            {
                if (BitConverter.IsLittleEndian)
                    Array.Reverse(_bytes);
                _data = BitConverter.ToInt32(_bytes, 0);
                Log.Write("Recieved: " + _data.ToString(), ConsoleColor.Cyan);

                //Send back to client
                string msg = _data.ToString().ToUpper();

                byte[] bmsg = System.Text.Encoding.ASCII.GetBytes(msg);
                stream.Write(bmsg, 0, msg.Length);
                Log.Write("Sent: " + _data);
            }

            _client.Close();
        }
    }
}