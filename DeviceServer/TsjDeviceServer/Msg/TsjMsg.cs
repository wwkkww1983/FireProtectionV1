using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using TsjDeviceServer.Data;

namespace TsjDeviceServer.Msg
{
    public class TsjMsg
    {
        TsjTopic _tsjTopic;
        byte[] _payload;
        public TsjData TsjData { get; set; }
        public bool IsAlarm { get; private set; }
        public bool IsFault { get; private set; }
        public bool IsMonitor { get; private set; }
        public bool IsOverflow { get; private set; }
        public bool IsOffline { get; private set; }
        public bool IsHello { get; private set; }
        public string DeviceSn { get; private set; }

        public TsjMsg(string topic,byte[] payload)
        {
            _tsjTopic = new TsjTopic(topic);
            _payload = payload;
            if (payload == null)
            {
                Logger.Log.Error("payload is null");
                return;
            }
            var strload = Encoding.UTF8.GetString(_payload);
            Logger.Log.Info($"{topic}\r\n{strload}");
            try
            {
                DeviceSn = _tsjTopic.Level(2);
                if (_tsjTopic.Level(3).Equals("Alarm"))
                {
                    TsjData = JsonConvert.DeserializeObject<DataAlarm>(strload);
                    IsAlarm = true;
                }else if (_tsjTopic.Level(3).Equals("Fault"))
                {
                    TsjData = JsonConvert.DeserializeObject<DataFault>(strload);
                    IsFault = true;
                }
                else if (_tsjTopic.Level(3).Equals("Monitor"))
                {
                    TsjData = JsonConvert.DeserializeObject<DataMonitor>(strload);
                    IsMonitor = true;
                }
                else if (_tsjTopic.Level(3).Equals("Overflow"))
                {
                    TsjData = JsonConvert.DeserializeObject<DataOverflow>(strload);
                    IsOverflow = true;
                }
                else if (_tsjTopic.Level(3).Equals("Will"))
                {
                    IsOffline = true;
                }
                else if (_tsjTopic.Level(3).Equals("Hello"))
                {
                    TsjData = JsonConvert.DeserializeObject<DataHello>(strload);
                    IsHello = true;
                }
            }
            catch (Exception e)
            {
                Logger.Log.Error(e.Message);
            }
        }
    }
}
