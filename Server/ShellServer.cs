using System;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Collections.Generic;
using System.Threading;

namespace SocketTest.Server
{
    public class ShellServer
    {
        public static string IPString = "127.0.0.1";
        public static int Port = 1337;

        public static void Start()
        {
            IPAddress localIP = IPAddress.Parse(IPString);

            TcpListener server = new TcpListener(localIP, Port);
            server.Start();

            Byte[] bytes = new Byte[256];
            string data = null;

            List<TcpClient> clientList = new List<TcpClient>();
            while(true)
            {
                Log.Write("Waiting for connection...");
                TcpClient client = server.AcceptTcpClient();
                Log.Success("Connected! ");

                //Update list (Currently useless)
                clientList.Add(client);

                Thread t = new Thread(new ThreadStart(()=>ConnectClient(client,bytes,data))); 
                t.Start();
            }
            Log.Write("Finished");
        }

        public static void ConnectClient(TcpClient _client, byte[] _bytes, string _data)
        {
             _data = null;
        
                NetworkStream stream = _client.GetStream();
                int i;
                while((i = stream.Read(_bytes,0,_bytes.Length))!=0)
                {
                    _data = System.Text.Encoding.ASCII.GetString(_bytes,0,i);
                    Log.Write("Recieved: "+_data, ConsoleColor.Cyan);

                    //Send back to client
                    _data = _data.ToUpper();

                    byte[] msg = System.Text.Encoding.ASCII.GetBytes(_data);
                    stream.Write(msg,0,msg.Length);
                    Log.Write("Sent: "+_data);
                }

                _client.Close();
        }
    }
}