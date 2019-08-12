using System;
using System.Collections.Generic;
using System.Text;

namespace DeviceServer.Tcp.Protocol.Datas
{
    class DataUnitState: DataObject
    {
        ///// <summary>
        ///// 系统类型标志
        ///// </summary>
        //byte SysType;
        ///// <summary>
        ///// 系统地址
        ///// </summary>
        //byte SysAddress; 
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
        public string UnitAddressString { get { return $"{_data[3]}.{_data[4]}.{_data[5]}.{_data[6]}"; } }

        ///// <summary>
        ///// 部件状态
        ///// </summary>
        //byte[] UnitState = new byte[2];
        ///// <summary>
        ///// 部件说明
        ///// </summary>
        //byte[] UnitDescription = new byte[31];

        public DataUnitState(List<byte> data) : base(data)
        {

        }

        /// <summary>
        /// 是否火警
        /// </summary>
        /// <returns></returns>
        public bool Alarm()
        {
            return 1 == ((_data[7] >> 1) & 1);
        }
        /// <summary>
        /// 是否故障
        /// </summary>
        /// <returns></returns>
        public bool Fault()
        {
            return 1 == ((_data[7] >> 2) & 1);
        }
        /// <summary>
        /// 是否电源故障
        /// </summary>
        /// <returns></returns>
        public bool FaultPower()
        {
            return 1 == (_data[8] & 1);
        }
    }
}
