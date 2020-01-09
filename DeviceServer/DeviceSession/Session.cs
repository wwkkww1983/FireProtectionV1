using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DeviceSession
{
    public class Session
    {
        public event Action<byte> OnRecv;
        public event Action<Session> OnDisconnect;
        public System.Net.EndPoint RemoteEndPoint { get { return _client.Client.RemoteEndPoint; } }
        TcpClient _client;
        Thread _tRead;
        bool _isRun = false;
        static public Session Create(TcpClient client)
        {
            Session ss = new Session() { _client = client };
            client.GetStream().ReadTimeout = 500;
            ss._tRead = new Thread(ss.Read);
            ss._isRun = true;
            ss._tRead.Start();
            return ss;
        }

        public Task SendAsync(byte[] data)
        {
            return _client.GetStream().WriteAsync(data, 0, data.Length);
        }

        private void Read(object obj)
        {
            byte[] byteRead = new byte[1];
            while (_isRun)
            {
                int readlen = -1;
                try
                {
                    readlen = _client.Client.Receive(byteRead);
                    //string s = byteRead[0].ToString("X2") + " ";
                    //TestAjsDeviceServer.Log(byteRead[0]);
                    //Console.Write(s);
                }catch(SocketException e)
                {
                    if(e.SocketErrorCode== SocketError.TimedOut)
                        continue;
                    if (!_client.Connected)
                        OnDisconnect?.Invoke(this);
                    _client.Close();
                    break;
                }catch(Exception e)
                {
                    if (!_client.Connected)
                        OnDisconnect?.Invoke(this);
                    _client.Close();
                    break;
                }
                if (readlen<1)
                    continue;
                OnRecv?.Invoke(byteRead[0]);
            }
        }

        public void Stop()
        {
            _isRun = false;
            _client.Close();
            _tRead.Join();
        }
    }
}
