using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace julienfEngine1
{
    class Client<T> where T : unmanaged
    {
        private IPHostEntry _host;
        private IPAddress _ipAddress;
        private IPEndPoint _ipEndPoint;

        private Socket _socketClient;

        byte[] _messageInBytes;

        public Client(string ip, int port)
        {
            _host = Dns.GetHostEntry(ip);
            _host.AddressList = Dns.GetHostAddresses(ip);
            _ipAddress = _host.AddressList[0];
            _ipEndPoint = new IPEndPoint(_ipAddress, port);

            _socketClient = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            unsafe
            {
                _messageInBytes = new byte[sizeof(T)];
            }
        }

        public void ConnectWithServer()
        {
            _socketClient.Connect(_ipEndPoint);
        }

        public Task ConnectWithServerAsync()
        {
            Task taskResult = Task.Run(() => _socketClient.Connect(_ipEndPoint));
            return taskResult;
        }

        public void Disconnect()
        {
            _socketClient.Shutdown(SocketShutdown.Both);
            _socketClient.Close();
        }

        public void SendInfo(string message)
        {
            _messageInBytes = Encoding.ASCII.GetBytes(message);

            _socketClient.Send(_messageInBytes);
        }

        public Task SendInfoAsync(string message)
        {
            Task taskResult = Task.Run(() =>
            {
                _messageInBytes = Encoding.ASCII.GetBytes(message);

                _socketClient.Send(_messageInBytes);
            });
            return taskResult;
        }

        public bool P_IsConncected
        {
            get
            {
                return _socketClient.Connected;
            }
        }

        ~Client()
        {
            Disconnect();
        }
    }
}
