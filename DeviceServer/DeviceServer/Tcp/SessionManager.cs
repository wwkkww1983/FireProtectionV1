using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace DeviceServer.Tcp
{
    class SessionManager
    {
        object _lock=new object();
        List<Session> _lstSession = new List<Session>();
        public SessionManager(Disposal disposal)
        {
            _disposal = disposal;
        }

        public Disposal _disposal { get; }

        public void Stop()
        {
            lock (_lock)
            {
                foreach(var ss in _lstSession)
                {
                    ss.Stop();
                }
            }
        }

        internal void AddSession(TcpClient client)
        {
            Session session = Session.Create(client);
            if (session != null)
            {
                session.OnData += _disposal.OnData;
                session.OnDisconnect += Session_OnDisconnect;
                lock (_lock)
                {
                    _lstSession.Add(session);
                    Console.WriteLine("上线"+session.RemoteEndPoint.ToString());
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
        }
    }
}
