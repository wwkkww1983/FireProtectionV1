using Common;
using FireProtectionV1.TsjDevice.Dto;
using MQTTnet;
using MQTTnet.Client.Receiving;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TsjDeviceServer.Data;
using TsjDeviceServer.Msg;
using TsjWebApi;

namespace TsjDeviceServer
{
    class MsgRecvHandler : IMqttApplicationMessageReceivedHandler
    {
        public async Task HandleApplicationMessageReceivedAsync(MqttApplicationMessageReceivedEventArgs e)
        {
            //Console.WriteLine("### RECEIVED APPLICATION MESSAGE ###");
            //Console.WriteLine($"+ Topic = {e.ApplicationMessage.Topic}");
            //var str = e.ApplicationMessage.Payload == null ? "" : Encoding.UTF8.GetString(e.ApplicationMessage.Payload);
            //Console.WriteLine($"+ Payload = {str}");
            //Console.WriteLine($"+ QoS = {e.ApplicationMessage.QualityOfServiceLevel}");
            //Console.WriteLine($"+ Retain = {e.ApplicationMessage.Retain}");
            //Console.WriteLine();

            //var topic = e.ApplicationMessage.Topic;
            //var payload= e.ApplicationMessage.Payload == null ? "" : Encoding.UTF8.GetString(e.ApplicationMessage.Payload);
            TsjMsg tsjMsg = new TsjMsg(e.ApplicationMessage.Topic, e.ApplicationMessage.Payload);
            if (tsjMsg.IsAlarm)
            {
                var msg = (DataAlarm)tsjMsg.TsjData;
                FireApi.HttpPostTsj(Config.Url("/api/services/app/TsjDevice/NewAlarm"), new NewAlarmInput()
                {
                    Identify = msg.id,
                    Time = TsjConvert.ToLocalDateTime(msg.at).ToString("yyyy-MM-dd HH:mm:ss")
                });
            }
            else if (tsjMsg.IsFault)
            {
                var msg = (DataFault)tsjMsg.TsjData;
                FireApi.HttpPostTsj(Config.Url("/api/services/app/TsjDevice/NewFault"), new NewFaultInput()
                {
                    Identify = msg.id,
                    Time = TsjConvert.ToLocalDateTime(msg.at).ToString("yyyy-MM-dd HH:mm:ss")
                });
            }
            else if (tsjMsg.IsMonitor)
            {
                var msg = (DataMonitor)tsjMsg.TsjData;
                FireApi.HttpPostTsj(Config.Url("/api/services/app/TsjDevice/NewMonitor"), new NewMonitorInput()
                {
                    Identify = msg.id,
                    Value=msg.value,
                    Time = TsjConvert.ToLocalDateTime(msg.at).ToString("yyyy-MM-dd HH:mm:ss")
                });
            }
            else if (tsjMsg.IsOverflow)
            {
                var msg = (DataOverflow)tsjMsg.TsjData;
                FireApi.HttpPostTsj(Config.Url("/api/services/app/TsjDevice/NewOverflow"), new NewOverflowInput()
                {
                    Identify = msg.id,
                    Value = msg.value,
                    Time = TsjConvert.ToLocalDateTime(msg.at).ToString("yyyy-MM-dd HH:mm:ss")
                });
            }

        }
    }
}
