using System;
using System.Collections.Generic;
using System.Text;

namespace DeviceServer.Tcp.Protocol.Datas
{
    class DataUploadAnalog:DataObject
    {
        /// <summary>
        /// 部件类型
        /// </summary>
        public byte UnitType { get { return _data[2]; } }
        /// <summary>
        /// 部件地址
        /// </summary>
        public List<byte> UnitAddress { get { return _data.GetRange(3, 4); } }
        /// <summary>
        /// 部件地址字符串
        /// </summary>
        public string UnitAddressString { get { return $"{_data[6]}.{_data[5]}.{_data[4]}.{_data[3]}"; } }
        /// <summary>
        /// 模拟量值
        /// </summary>
        public double Analog { get {
                double v = 0;
                if (BitConverter.IsLittleEndian)
                    v=(double)(_data[8] | _data[9] << 8);
                else
                    v=(double)(_data[8] >> 8 | _data[9]);
                if (_data[7] == (byte)AnalogType.Temperature || _data[7] == (byte)AnalogType.ResidualAjs)
                    v = v * 0.1;
                return v;
            }
        }
        public AnalogType AnalogType { get {
                if (Enum.IsDefined(typeof(AnalogType), _data[7]))
                    return (AnalogType)_data[7];
                else
                    return AnalogType.Unknown;
            } }
        /// <summary>
        /// 模拟量单位
        /// </summary>
        public string AnalogUnit { get {
                string v = "";
                if (_data[7] == (byte)AnalogType.Temperature)
                    v= "℃";
                else if (_data[7] == (byte)AnalogType.ResidualAjs)
                    v= "A";
                return v;
            } }
        public DataUploadAnalog(List<byte> list) : base(list)
        {

        }
    }
}
