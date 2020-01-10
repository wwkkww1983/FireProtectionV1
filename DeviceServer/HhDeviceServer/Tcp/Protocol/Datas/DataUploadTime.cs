using System;
using System.Collections.Generic;
using System.Text;

namespace DeviceServer.Tcp.Protocol.Datas
{
    /// <summary>
    /// 上传用户信息传输装置系统时间 上行
    /// </summary>
    class DataUploadTime : DataObject
    {
        public DataUploadTime(List<byte> list):base(list)
        {
        }
    }
}
