using DeviceServer.Tcp.Protocol.Datas;
using System;
using System.Collections.Generic;
using System.Text;

namespace DeviceServer.Tcp.Protocol
{
    class DataPacket
    {
        /// <summary>
        /// 数据类型
        /// </summary>
        public DataType DataType { get; private set; }
        /// <summary>
        /// 数据对象数量
        /// </summary>
        public byte DataCount { get;private set; }
        /// <summary>
        /// 数据对象数组
        /// </summary>
        public List<DataObject> Datas=new List<DataObject>();

        public DataPacket(List<byte> dataUnit)
        {
            if (!Enum.IsDefined(typeof(DataType), dataUnit[0]))
                return;
            DataType = (DataType)dataUnit[0];
            DataCount = dataUnit[1];
            switch (DataType)
            {
                case DataType.UploadUITDOperatInfo:
                    Datas.Add(new DataUITDOperatInfo(dataUnit.GetRange(2 , 8)));
                    break;
                case DataType.UploadUnitState:
                    Datas.Add(new DataUnitState(dataUnit.GetRange(2,dataUnit.Count-2)));
                    break;
                case DataType.UploadUnitAnalog:
                    Datas.Add(new DataUploadAnalog(dataUnit.GetRange(2, dataUnit.Count - 2)));
                    break;
            }
        }
    }
}
