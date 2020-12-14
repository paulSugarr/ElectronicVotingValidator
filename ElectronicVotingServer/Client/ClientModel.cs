using System;
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
                    //CommandManager.TryExecute(command, this, Server);
                    var msg = Encoding.UTF8.GetString(command);
                    Console.WriteLine(msg);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    break;
                }
            }
            Server.RemoveConnection(this.Id);
            Close();
        }

        private byte[] GetMessage()
        {
            byte[] data = new byte[64];
            int bytes = 0;
            int i = 0;
            
            do
            {
                i++;
                bytes = Stream.Read(data, 0, data.Length);

            }
            while (Stream.DataAvailable && _client.Client.Connected);
            return data;
        }

        protected internal void Close()
        {
            if (Stream != null)
                Stream.Close();
            if (_client != null)
                _client.Close();
        }
        private void OnConnect()
        {

        }
    }
}
