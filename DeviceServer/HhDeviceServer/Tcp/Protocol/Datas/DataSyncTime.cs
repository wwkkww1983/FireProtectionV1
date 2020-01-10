using System;
using System.Collections.Generic;
using System.Text;

namespace DeviceServer.Tcp.Protocol.Datas
{
    /// <summary>
    /// 同步用户信息传输装置系统时间 下行
    /// </summary>
    class DataSyncTime : DataObject
    {
        public DataSyncTime():base(new List<byte>()
        {
            (byte)DateTime.Now.Second,
            (byte)DateTime.Now.Minute,
            (byte)DateTime.Now.Hour,
            (byte)DateTime.Now.Day,
            (byte)DateTime.Now.Month,
            (byte)(DateTime.Now.Year-(DateTime.Now.Year/100)*100)
        })
        {

        }
    }
}
