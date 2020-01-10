using System;
using System.Collections.Generic;
using System.Text;

namespace DeviceServer.Tcp.Protocol.Datas
{
    class DataUITDOperatInfo: DataObject
    {
        ///// <summary>
        ///// 操作标志
        ///// </summary>
        //byte Operation;
        ///// <summary>
        ///// 操作员
        ///// </summary>
        //byte Operator;

        public DataUITDOperatInfo(List<byte> data) : base(data)
        {

        }

        /// <summary>
        /// 是否手动报警
        /// </summary>
        /// <returns></returns>
        public bool AlarmManual()
        {
            return 1 == ((_data[0] >> 2) & 1);
        }
        /// <summary>
        /// 是否警情消除
        /// </summary>
        /// <returns></returns>
        public bool AlarmCancel()
        {
            return 1 == ((_data[0] >> 3) & 1);
        }
    }
}
