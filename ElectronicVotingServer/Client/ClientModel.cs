using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using ElectronicVotingServer.Server;

namespace ElectronicVotingServer.Client
{
    public class ClientModel
    {
        protected internal string Id { get; private set; }
        protected internal NetworkStream Stream { get; private set; }
        public string UserName = "<unnamed>";
        private TcpClient _client;
        public ServerModel Server;

        public ClientModel(TcpClient tcpClient, ServerModel serverModel, string id)
        {
            Id = id;
            _client = tcpClient;
            Server = serverModel;
            serverModel.AddConnection(this);
            Stream = _client.GetStream();
        }



        public void Process()
        {
            try
            {
                OnConnect();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Server.RemoveConnection(this.Id);
                Close();
                return;
            }


            while (true)
            {

                try
                {
                    var command = GetMessage();
                    if (command.Length < 2) throw new Exception("Short message");
                    Console.WriteLine($"source = {((IPEndPoint)_client.Client.LocalEndPoint).Address}, lenght = {command.Length}");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    break;
                }
            }

            Close();
            Server.RemoveConnection(this.Id);
        }

        private string GetMessage()
        {
            byte[] data = new byte[250];
            StringBuilder builder = new StringBuilder();
            int bytes = 0;
            do
            {
                bytes = Stream.Read(data, 0, data.Length);
                builder.Append(Encoding.UTF8.GetString(data, 0, bytes));
            }
            while (Stream.DataAvailable);
 
            return builder.ToString();
        }

        protected internal void Close()
        {
            Console.WriteLine("close");
            Stream?.Close();
            _client?.Close();
        }
        private void OnConnect()
        {
            Console.WriteLine("шото подключилось");
        }
    }
}
