using System;
using System.Threading;
using ElectronicVotingServer.Server;

namespace TCPServer
{
    class Program
    {
        static ServerModel _server;
        static Thread _listenThread;
        static void Main(string[] args)
        {
            {
                try
                {
                    _server = new ServerModel();
                    _listenThread = new Thread(_server.Listen);
                    _listenThread.Start();
                }
                catch (Exception ex)
                {
                    _server.Disconnect();
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}
