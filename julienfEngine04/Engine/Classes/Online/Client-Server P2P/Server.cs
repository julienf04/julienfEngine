using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace julienfEngine1
{
    class Server<T> where T : unmanaged
    {
        private IPHostEntry _host;
        private IPAddress _ipAddress;
        private IPEndPoint _ipEndPoint;
         
        private Socket _socketServer;
        private Socket _socketClient;

        private byte[] _messageInBytes;

        public Server(string ip, int port)
        {
            _host = Dns.GetHostEntry(ip);
            _host.AddressList = Dns.GetHostAddresses(ip);
            for (int i = 0; i < _host.AddressList.Length; i++)
                if (_host.AddressList[i].AddressFamily == AddressFamily.InterNetwork) _ipAddress = _host.AddressList[i];

            _ipEndPoint = new IPEndPoint(_ipAddress, port);
            _socketServer = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            _socketServer.Bind(_ipEndPoint);
            _socketServer.Listen(1);

            unsafe
            {
                _messageInBytes = new byte[sizeof(T)];
            }
        }

        public void ConnectWithClient()
        {
            _socketClient = _socketServer.Accept();
        }

        public Task ConnectWithClientAsync()
        {
            Task taskResult = Task.Run(() => _socketClient = _socketServer.Accept());
            return taskResult;
        }

        public void Disconnect()
        {
            _socketClient.Shutdown(SocketShutdown.Both);
            _socketServer.Shutdown(SocketShutdown.Both);
            _socketClient.Close();
            _socketServer.Close();
        }

        public string ReciveInfo()
        {
            string messageInString = "";

            _socketClient.Receive(_messageInBytes);

            messageInString = Encoding.ASCII.GetString(_messageInBytes);

            return messageInString;
        }

        public Task<string> ReceiveInfoAsync()
        {
            Task<string> taskResult = Task.Run<string>(() =>
            {
                string messageInString;

                _socketClient.Receive(_messageInBytes);

                messageInString = Encoding.ASCII.GetString(_messageInBytes);

                return messageInString;
            });

            return taskResult;
        }

        ~Server()
        {
            Disconnect();
        }
    }
}
