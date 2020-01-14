using System;
using System.Collections.Generic;
using System.Text;

namespace DeviceServer.Tcp.Protocol
{
    public class Packet
    {
        public List<byte> Data { get { return _lst; } }
        List<byte> _lst = new List<byte>();
        /// <summary>
        /// 帧头，固定为两个字节
        /// </summary>
        public List<byte> Head { get { return _lst.GetRange(0, 2); } }
        /// <summary>
        /// 流水号
        /// </summary>
        public List<byte> Sn { get { return _lst.GetRange(2, 2); } }
        /// <summary>
        /// 版本号
        /// </summary>
        public List<byte> Version { get { return _lst.GetRange(4, 2); } }
        /// <summary>
        /// 时间标签（14 08 11 18 05 13，表示13年5月18号11点08分14秒）
        /// </summary>
        public List<byte> TimePack { get { return _lst.GetRange(6, 6); } }
        /// <summary>
        /// 网关标识（6A 15 83 B6 00 00）
        /// </summary>
        public List<byte> SrcAddress { get { return _lst.GetRange(12, 6); } }
        /// <summary>
        /// 网关标识字符串（其实就是把网关标识的6位数字倒过来，以.分隔：00.00.B6.83.15.6A,恒华是10位整数）
        /// </summary>
        public string SrcAddressString
        {
            get
            {
                byte[] bts = new byte[4] { _lst[17], _lst[16], _lst[15], _lst[14] };
                int sn = byteArrayToInt(bts);
                return sn.ToString("0000000000");
            }
        }
        //public string SrcAddressString { get { return $"{_lst[17]}.{_lst[16]}.{_lst[15]}.{_lst[14]}.{_lst[13]}.{_lst[12]}"; } }
        /// <summary>
        /// 目的地址（51 1F 00 00 00 0B）
        /// </summary>
        public List<byte> DstAddress { get { return _lst.GetRange(18, 6); } }
        /// <summary>
        /// 数据单元长度
        /// </summary>
        public ushort DataUnitLen { get; private set; }
        /// <summary>
        /// 控制命令原始值（01/02/03/07/08）
        /// </summary>
        public byte CmdByte { get { return _lst[26]; } }
        /// <summary>
        /// 控制命令类型（根据控制命令原始值返回枚举型的Cmd，例如原始值02就是Cmd.Send）
        /// </summary>
        public Cmd Cmd
        {
            get
            {
                if (!Enum.IsDefined(typeof(Cmd), _lst[26]))
                    return Cmd.Unknown;
                return (Cmd)_lst[26];
            }
        }
         int byteArrayToInt(byte[] bytes)
        {
            int value = 0;
            // 由高位到低位  
            for (int i = 0; i < 4; i++)
            {
                int shift = (4 - 1 - i) * 8;
                value += (bytes[i] & 0x000000FF) << shift;// 往高位游  
            }
            return value;
        }
        /// <summary>
        /// 应用数据单元
        /// </summary>
        public List<byte> DataUnit { get { return _lst.GetRange(27, DataUnitLen); } }
        /// <summary>
        /// 校验和（1个字节）
        /// </summary>
        public byte Check { get { return _lst[27 + DataUnitLen]; } }
        /// <summary>
        /// 结束位（固定为两个字节）
        /// </summary>
        public List<byte> End { get { return _lst.GetRange(27 + DataUnitLen + 1, 2); } }


        /// <summary>
        /// 接收数据（一个字节一个字节的接收）
        /// </summary>
        /// <param name="bt"></param>
        /// <returns></returns>
        internal bool Recv(byte bt)
        {
            // 把接收到的字节逐一加到List中
            // 确保帧头的两位正确（List[0]和List[1])
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
            // 前27位字节都只管接收即可（接收了帧头、流水号、版本、时间标签、网关标识、目的地址）
            if (27 > _lst.Count)
                return false;

            if (27 == _lst.Count)//数据长度
            {
                if (BitConverter.IsLittleEndian)
                    DataUnitLen = (ushort)(_lst[24] | _lst[25] << 8);
                else
                    DataUnitLen = (ushort)(_lst[24] >> 8 | _lst[25]);
            }
            // 最后3位是校验位加上固定两位的结束符
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
                    return true;    // 直到返回True，表示一条数据接收完整
                }
            }
            return false;
        }
    }
}
