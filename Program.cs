using System;
using SocketTest.Server;
using SocketTest.Client;
using System.Threading;

namespace SocketTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Log.Write("<- Pick your Poison ->", ConsoleColor.Blue);
            Log.Write("(1) String Server\n(2) Data Server\n(3) String Client \n(4) Data Client\n(5) Netcat Client", ConsoleColor.DarkBlue);

            string command = "";
            while (!command.Contains("quit") && !command.Contains("1") && !command.Contains("2") && !command.Contains("3") && !command.Contains("4") && !command.Contains("5"))
            {
                command = Console.ReadLine();
            }
            switch (command)
            {
                case "1":
                    Log.Write("Starting server");
                    CreateServer();
                    break;
                case "2":
                    Log.Write("Starting data server");
                    CreateDataServer();
                    break;
                case "3":
                    Log.Write("Starting string client");
                    CreateClient();
                    break;
                case "4":
                    Log.Write("Starting data client");
                    CreateDataClient();
                    break;
                case "5":
                    Log.Write("Starting nc connection");
                    CreateNCClient();
                    break;
            }
        }

        public static void CreateServer()
        {
            ShellServer.Start();
        }

        public static void CreateDataServer()
        {
            DataServer.Start();
        }

        public static void CreateClient()
        {
            ShellClient.Start();
        }

        public static void CreateDataClient()
        {
            DataClient.Start();
        }

        public static void CreateNCClient()
        {
            NCClient.Start();
        }
    }
}
