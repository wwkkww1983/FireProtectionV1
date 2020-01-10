using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DeviceServer.Tcp;

namespace DeviceServer.Tcp
{
    class TransTcp
    {
        Task _taskConn;
        bool _bConn = false;
        private string _identify;
        private string _ip;
        private int _port;
        TcpClient _tcpClient;
        bool _bClose = true;
        Thread _threadRecv;
        private DateTime _lastTime;

        public Session DeviceSession { get; internal set; }

        public TransTcp(string identify, string ip, int port, int localport)
        {
            _identify = identify;
            this._ip = ip;
            this._port = port;
            _tcpClient = new TcpClient(new IPEndPoint(IPAddress.Any,localport));
            _tcpClient.ReceiveTimeout = 500;
            _tcpClient.SendTimeout = 500;
            _bClose = false;
            var t=ConnectAsync();
        }

        private async Task ConnectAsync()
        {
                if (_taskConn != null)
                    _taskConn.Wait();
            try
            {
                if (_bClose)
                    return;
                if (_tcpClient.Connected)
                    _tcpClient.Close();
                Console.WriteLine($"转发{_identify}正在连接安吉斯服务器ip:{_ip}端口:{_port}");
                _taskConn = _tcpClient.ConnectAsync(_ip, _port);
            }
            catch (SocketException e)
            {
                if (e.SocketErrorCode == SocketError.IsConnected)
                    return;
                Console.WriteLine(e.Message);
                Thread.Sleep(3000);
                await ConnectAsync();
            }
            _taskConn.Wait();
            Console.WriteLine($"转发{_identify}连接安吉斯服务器ip:{_ip}端口:{_port}成功");
            _threadRecv = new Thread(Recv);
            _threadRecv.Start();
        }

        public void Close()
        {
            _bClose = true;
            _threadRecv.Join();
        }

        public async Task Send(byte[] data)
        {
            try
            {
                int nSend= await _tcpClient.Client.SendAsync(data,SocketFlags.None);
            }
            catch (SocketException e)
            {
                if((DateTime.Now-_lastTime).Equals(new TimeSpan(0,30,0)))
                    await ConnectAsync();
            }
        }

        private void Recv(object obj)
        {
            byte[] recv = new byte[500];
            while (!_bClose)
            {
                try
                {
                    int n= _tcpClient.Client.Receive(recv);
                    if (n > 0)
                    {
                        _lastTime = DateTime.Now;
                        DeviceSession.Send(recv,0, n);
                    }
                }catch(SocketException e)
                {
                    if (e.SocketErrorCode == SocketError.TimedOut)
                        continue;
                    break;
                }
            }
        }
    }
}
