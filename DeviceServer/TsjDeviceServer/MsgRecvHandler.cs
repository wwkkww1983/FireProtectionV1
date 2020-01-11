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
            else if (tsjMsg.IsMonitor || tsjMsg.IsOverflow)
            {
                var msg = (DataMonitor)tsjMsg.TsjData;
                FireApi.HttpPostTsj(Config.Url("/api/services/app/FireDevice/AddElecRecord"), new NewMonitorInput()
                {
                    fireElectricDeviceSn= tsjMsg.DeviceSn,
                    sign = msg.id,
                    analog=msg.value
                });
            }
            else if (tsjMsg.IsOffline)
            {
                var msg = (DataOverflow)tsjMsg.TsjData;
                var gatewayType = tsjMsg.DeviceSn.Contains("TSJ-DQ") ? 2 : 1;
                FireApi.HttpPostTsj(Config.Url("/api/services/app/FireDevice/UpdateDeviceState"), new 
                {
                    gatewayType,
                    gatewaySn = tsjMsg.DeviceSn,
                    gatewayStatus = -1
                });
            }
            else if (tsjMsg.IsHello)
            {
                var msg = (DataOverflow)tsjMsg.TsjData;
                var gatewayType = tsjMsg.DeviceSn.Contains("TSJ-DQ") ? 2 : 1;
                FireApi.HttpPostTsj(Config.Url("/api/services/app/FireDevice/UpdateDeviceState"), new
                {
                    gatewayType,
                    gatewaySn = tsjMsg.DeviceSn,
                    gatewayStatus = 1
                });
            }
        }
    }
}
