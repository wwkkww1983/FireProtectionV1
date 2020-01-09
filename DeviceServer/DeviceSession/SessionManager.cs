using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace DeviceSession
{
    public class SessionManager
    {
        public event Action<Session> OnStartSession;
        public event Action<Session> OnEndSession;
        SrvConnect _tcpSrv = new SrvConnect();
        object _lock=new object();
        List<Session> _lstSession = new List<Session>();
        public SessionManager()
        {
            _tcpSrv.OnConnected += _tcpSrv_OnConnected;
        }

        private void _tcpSrv_OnConnected(TcpClient client)
        {
            Session session = Session.Create(client);
            if (session != null)
            {
                OnStartSession?.Invoke(session);
                session.OnDisconnect += Session_OnDisconnect;
                lock (_lock)
                {
                    _lstSession.Add(session);
                    Console.WriteLine("上线" + session.RemoteEndPoint.ToString());
                }
            }
        }

        public string Start(int port)
        {
            return _tcpSrv.Start(port);
        }
        public void Stop()
        {
            _tcpSrv.Stop();
            lock (_lock)
            {
                foreach(var ss in _lstSession)
                {
                    ss.Stop();
                }
            }
        }
        
        private void Session_OnDisconnect(Session session)
        {
            lock (_lock)
            {
                Console.WriteLine("离线"+session.RemoteEndPoint.ToString());
                _lstSession.Remove(session);
            }
            OnEndSession?.Invoke(session);
        }
    }
}
