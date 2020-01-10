using DeviceServer.Tcp.Protocol;
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DeviceServer.Tcp
{
    public class Session
    {
        /// <summary>
        /// 是否转发
        /// </summary>
        public bool IsTransfer { get; set; }
        /// <summary>
        /// 下发数据流水号
        /// </summary>
        public ushort _sendSn=0;
        public event Action<Session, Packet> OnData;
        public event Action<Session> OnDisconnect;
        public System.Net.EndPoint RemoteEndPoint { get { return _client.Client.RemoteEndPoint; } }
        //List<byte> _lst = new List<byte>();
        Packet _packRecv=new Packet();
        /// <summary>
        /// 下发数据
        /// </summary>
        /// <param name="recv"></param>
        /// <param name="cmd"></param>
        /// <param name="dataType"></param>
        /// <param name="datas"></param>
        internal void SendPacket(Packet recv, Cmd cmd, DataType dataType, List<DataObject> datas)
        {
            if (IsTransfer)
                return;
            List<byte> send = new List<byte>();
            send.AddRange(recv.Head);
            _sendSn++;
            if (BitConverter.IsLittleEndian)
            {
                send.Add((byte)_sendSn);
                send.Add((byte)(_sendSn >> 8));
            }
            else
            {
                send.Add((byte)(_sendSn >> 8));
                send.Add((byte)_sendSn);
            }
            send.AddRange(recv.Version);
            //时间标签
            var now = DateTime.Now;
            send.Add((byte)now.Second);
            send.Add((byte)now.Minute);
            send.Add((byte)now.Hour);
            send.Add((byte)now.Day);
            send.Add((byte)now.Month);
            send.Add((byte)now.Year);
            //源地址
            send.AddRange(recv.DstAddress);
            //目的地址
            send.AddRange(recv.SrcAddress);
            //长度
            ushort datalen = 2;
            foreach(var d in datas)
            {
                datalen += d.DataLen;
            }
            if (BitConverter.IsLittleEndian)
            {
                send.Add((byte)datalen);
                send.Add((byte)(datalen >> 8));
            }
            else
            {
                send.Add((byte)(datalen >> 8));
                send.Add((byte)datalen);
            }
            //命令
            send.Add((byte)cmd);
            //数据类型
            send.Add((byte)dataType);
            //数据对象数量
            send.Add((byte)datas.Count);
            //数据对象
            foreach(var d in datas)
            {
                send.AddRange(d.DataBytes);
            }
            //校验
            send.Add(Method.CheckSum(send, 2, 25+datalen));
            send.Add(0x23);
            send.Add(0x23);
            var data = send.ToArray();
            _client.GetStream().WriteAsync(data, 0, data.Length);
        }

        internal void Send(byte[] data, int offset, int len)
        {
            _client.GetStream().WriteAsync(data, offset, len);
        }

        TcpClient _client;
        private Session() { }
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

        private void Read(object obj)
        {
            byte[] byteRead = new byte[1];
            while (_isRun)
            {
                int readlen = -1;
                try
                {
                    readlen = _client.Client.Receive(byteRead);
                    string s = byteRead[0].ToString("X2") + " ";
                    TestAjsDeviceServer.Log(byteRead[0]);
                    Console.Write(s);
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
                if (_packRecv.Recv(byteRead[0]))
                {
                    Packet pack = _packRecv;
                    //确认
                    Confirm(pack);
                    //处理
                    Task.Run(() => OnData?.Invoke(this, pack));
                    _packRecv = new Packet();
                }
            }
        }
        /// <summary>
        /// 消息传输确认
        /// </summary>
        /// <param name="recv"></param>
        private void Confirm(Packet recv)
        {
            if (IsTransfer)
                return;
            List<byte> send = new List<byte>();
            send.AddRange(recv.Head);
            send.AddRange(recv.Sn);
            send.AddRange(recv.Version);
            //时间标签
            var now = DateTime.Now;
            send.Add((byte)now.Second);
            send.Add((byte)now.Minute);
            send.Add((byte)now.Hour);
            send.Add((byte)now.Day);
            send.Add((byte)now.Month);
            send.Add((byte)now.Year);
            //源地址
            send.AddRange(recv.DstAddress);
            //目的地址
            send.AddRange(recv.SrcAddress);
            //长度
            send.Add(0);
            send.Add(0);
            //命令
            send.Add(recv.CmdByte == (byte)Cmd.Ajs07 ? (byte)Cmd.Ajs07 : (byte)Cmd.Confirm);
            //校验
            send.Add(Method.CheckSum(send, 2, 25));
            send.Add(0x23);
            send.Add(0x23);
            var data = send.ToArray();
            _client.GetStream().WriteAsync(data, 0, data.Length);
        }

        private void OnDataPrint(List<byte> list)
        {
            Console.WriteLine("\r\n[");
            foreach(var b in list)
            {
                Console.WriteLine(b.ToString("X2")+" ");
            }
            Console.WriteLine("]\r\n");
        }

        public void Stop()
        {
            _isRun = false;
            _client.Close();
            _tRead.Join();
        }
    }
}
