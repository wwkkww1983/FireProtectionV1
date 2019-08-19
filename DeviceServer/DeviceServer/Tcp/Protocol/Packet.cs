using System;
using System.Collections.Generic;
using System.Text;

namespace DeviceServer.Tcp.Protocol
{
    public class Packet
    {
        public List<byte> Data { get { return _lst; } }
        List<byte> _lst = new List<byte>();
        public List<byte> Head { get { return _lst.GetRange(0, 2); } }
        public List<byte> Sn { get { return _lst.GetRange(2, 2); } }
        public List<byte> Version { get { return _lst.GetRange(4, 2); } }
        public List<byte> TimePack { get { return _lst.GetRange(6, 6); } }
        public List<byte> SrcAddress { get { return _lst.GetRange(12, 6); } }
        public string SrcAddressString { get { return $"{_lst[17]}.{_lst[16]}.{_lst[15]}.{_lst[14]}.{_lst[13]}.{_lst[12]}"; } }
        public List<byte> DstAddress { get { return _lst.GetRange(18, 6); } }
        //byte[] Head     = new byte[2];
        //byte[] Sn       = new byte[2];
        //byte[] Version  = new byte[2];
        //byte[] TimePack = new byte[6];
        //byte[] SrcIP    = new byte[6];
        //byte[] DstIP    = new byte[6];
        /// <summary>
        /// 数据单元长度
        /// </summary>
        public ushort DataUnitLen { get;private set; }
        /// <summary>
        /// 控制命令原始值
        /// </summary>
        public byte CmdByte { get { return _lst[26]; } }
        /// <summary>
        /// 控制命令类型
        /// </summary>
        public Cmd Cmd { get {
                if (!Enum.IsDefined(typeof(Cmd), _lst[26]))
                    return Cmd.Unknown;
                return (Cmd)_lst[26];
            } }
        public List<byte> DataUnit { get { return _lst.GetRange(27, DataUnitLen); } } 
        public byte Check { get { return _lst[27 + DataUnitLen]; } }
        public List<byte> End { get { return _lst.GetRange(27 + DataUnitLen+1, 2); } }
        //byte[] End      = new byte[2];

        internal bool Recv(byte bt)
        {
            _lst.Add(bt);
            if (_lst[0] != 0x40)
            {
                _lst.Clear();
                return false;
            }
            if (1 == _lst.Count)
                return false;
            if (_lst[1] != 0x40)
            {
                _lst.Clear();
                return false;
            }
            if (27 > _lst.Count)
                return false;
            if (27 == _lst.Count)//数据长度
            {
                if (BitConverter.IsLittleEndian)
                    DataUnitLen = (ushort)(_lst[24] | _lst[25] << 8);
                else
                    DataUnitLen = (ushort)(_lst[24] >> 8 | _lst[25]);
            }
            if (_lst.Count == 27 + DataUnitLen + 3)
            {
                //byte check = Method.CheckSum(_lst, 2, 25 + len);
                //if (check != _lst[27 + len])//数据无效
                //{
                //    _lst.Clear();
                //    continue;
                //}
                //else
                {
                    return true;
                }
            }
            return false;
        }
    }
}
