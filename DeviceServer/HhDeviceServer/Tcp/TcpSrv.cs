using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace DeviceServer.Tcp
{
    class TcpSrv
    {
        TcpListener _listener;
        bool _isRun = false;
        Thread _threadAccept;
        SessionManager _manager;
        public TcpSrv(SessionManager manager)
        {
            _manager = manager;
        }
        public string Start(int port)
        {
            _listener = new TcpListener(IPAddress.Any, port);
            _listener.Start();
            _isRun = true;
            _threadAccept = new Thread(AcceptConnect);
            _threadAccept.Start();
            return _listener.LocalEndpoint.ToString();
        }
        private void AcceptConnect(object obj)
        {
            while (_isRun)
            {
                var client = _listener.AcceptTcpClient();
                _manager.AddSession(client);
            }
        }
        public void Stop()
        {
            _listener.Stop();
            _isRun = false;
            _threadAccept.Join();
        }
    }
}
