using System;
using System.Collections.Generic;
using System.Text;

namespace DeviceServer.Tcp
{
    public class Method
    {
        /// <summary>
        /// 打包数据加包头包尾
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="boxNum"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        static public byte[] Pack(byte cmd, byte boxNum, byte[] data = null)
        {
            if (data == null)
                data = new byte[0];
            List<byte> lst = new List<byte>();
            lst.Add(0xFC);
            lst.Add(cmd);
            lst.Add(boxNum);
            if (BitConverter.IsLittleEndian)
            {
                lst.Add((byte)(data.Length >> 8));
                lst.Add((byte)(data.Length));
            }
            else
            {
                lst.Add((byte)(data.Length));
                lst.Add((byte)(data.Length >> 8));
            }
            foreach (byte b in data)
            {
                lst.Add(b);
            }
            lst.Add(CheckSum(lst, 0, lst.Count));
            lst.Add(0xFD);
            return lst.ToArray();
        }

        /// <summary>
        /// 累计和校验
        /// </summary>
        /// <param name="lst"></param>
        /// <returns></returns>
        static public byte CheckSum(List<byte> lst, int startIndex, int count)
        {
            int res = 0;
            int endIndex = startIndex + count;
            for (int i = startIndex; i < endIndex; i++)
                res += lst[i];
            return (byte)res;
        }
    }
}
