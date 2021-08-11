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
            //Disable debug time
            Log.VerboseLevel = 0;
            Log.Write("  ______ ______                _   ", ConsoleColor.Red);
            Log.Write(" |  ____|___  /               | |  ", ConsoleColor.Red);
            Log.Write(" | |__     / / _ __ ___   ___ | |_ ", ConsoleColor.Red);
            Log.Write(@" |  __|   / / | '__/ _ \ / _ \| __|", ConsoleColor.Red);
            Log.Write(" | |____ / /__| | | (_) | (_) | |_ ", ConsoleColor.Red);
            Log.Write(@" |______/_____|_|  \___/ \___/ \__|", ConsoleColor.Red);
            Log.Write("(1) Server<string>", ConsoleColor.Yellow);
            Log.Write("(2) Server<int>", ConsoleColor.Yellow);
            Log.Write("(3) Client<string>", ConsoleColor.Cyan);
            Log.Write("(4) Client<int>", ConsoleColor.Cyan);
            Log.Write("(5) Reverse Shell <netcat>", ConsoleColor.Blue);

            string command = "";
            while (!command.Contains("quit") && !command.Contains("1") && !command.Contains("2") && !command.Contains("3") && !command.Contains("4") && !command.Contains("5"))
            {
                command = Console.ReadLine();
            }

            //Enable debug time
            Log.VerboseLevel = 1;

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
