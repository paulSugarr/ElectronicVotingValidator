using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Net;
using System.Threading;
using ElectronicVotingServer;
using ElectronicVotingServer.Server;

namespace TCPServer
{
    class Program
    {
        static ServerModel server;
        static Thread listenThread;
        static void Main(string[] args)
        {
            {
                try
                {
                    server = new ServerModel();
                    listenThread = new Thread(new ThreadStart(server.Listen));
                    listenThread.Start();
                }
                catch (Exception ex)
                {
                    server.Disconnect();
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}
