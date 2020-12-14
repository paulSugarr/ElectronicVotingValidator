using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using ElectronicVotingServer.Client;
using TCPServer;
using Factory;

namespace ElectronicVotingServer.Server
{
    public class ServerModel
    {
        private static TcpListener _tcpListener;
        private readonly List<ClientModel> _clients = new List<ClientModel>();
        public MainFactory MainFactory { get; } = new MainFactory();

        public ServerModel()
        {
            MainFactory.RegisterTypes();
        }

        protected internal void AddConnection(ClientModel clientModel)
        {
            _clients.Add(clientModel);
        }
        protected internal void RemoveConnection(string id)
        {
            ClientModel client = _clients.FirstOrDefault(c => c.Id == id);
            if (client != null)
                _clients.Remove(client);
        }

        protected internal void Listen()
        {
            try
            {
                _tcpListener = new TcpListener(IPAddress.Any, 8888);
                _tcpListener.Start();
                Console.WriteLine("Server run");

                while (true)
                {
                    TcpClient tcpClient = _tcpListener.AcceptTcpClient();

                    var clientModel = new ClientModel(tcpClient, this, _clients.Count.ToString());
                    var clientThread = new Thread(clientModel.Process);
                    clientThread.Start();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Disconnect();
            }
        }

        protected internal void BroadcastMessage(byte[] message, string id)
        {
            for (int i = 0; i < _clients.Count; i++)
            {
                if (_clients[i].Id != id) 
                {
                    _clients[i].Stream.Write(message, 0, message.Length);
                }
            }
        }

        protected internal void SendMessage(byte[] message, string id)
        {
            for (int i = 0; i < _clients.Count; i++)
            {
                if (_clients[i].Id == id)
                {
                    _clients[i].Stream.Write(message, 0, message.Length);
                }
            }
        }

        protected internal void Disconnect()
        {
            _tcpListener.Stop();

            for (int i = 0; i < _clients.Count; i++)
            {
                _clients[i].Close();
            }
            Environment.Exit(0);
        }
    }
}
