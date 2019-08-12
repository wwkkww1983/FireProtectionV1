using System;
using System.Collections.Generic;
using System.Text;

namespace DeviceServer.Tcp.Protocol
{
    abstract class DataObject
    {
        protected List<byte> _data = new List<byte>();
        /// <summary>
        /// 数据字节
        /// </summary>
        public List<byte> DataBytes { get { return _data; } }
        /// <summary>
        /// 数据对象时间标签
        /// </summary>
        //byte[] Time = new byte[6];
        protected DataObject(List<byte> data)
        {
            _data.AddRange(data);
        }
        /// <summary>
        /// 数据对象长度
        /// </summary>
        public ushort DataLen { get { return (ushort)_data.Count; } }
    }
}
