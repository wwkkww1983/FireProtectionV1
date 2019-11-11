using MQTTnet;
using MQTTnet.Client.Receiving;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TsjDeviceServer
{
    class MsgRecvHandler : IMqttApplicationMessageReceivedHandler
    {
        Protocol _protocol;
        public async Task HandleApplicationMessageReceivedAsync(MqttApplicationMessageReceivedEventArgs e)
        {
            //Console.WriteLine("### RECEIVED APPLICATION MESSAGE ###");
            //Console.WriteLine($"+ Topic = {e.ApplicationMessage.Topic}");
            //var str = e.ApplicationMessage.Payload == null ? "" : Encoding.UTF8.GetString(e.ApplicationMessage.Payload);
            //Console.WriteLine($"+ Payload = {str}");
            //Console.WriteLine($"+ QoS = {e.ApplicationMessage.QualityOfServiceLevel}");
            //Console.WriteLine($"+ Retain = {e.ApplicationMessage.Retain}");
            //Console.WriteLine();
            var topic = e.ApplicationMessage.Topic;
            var payload= e.ApplicationMessage.Payload == null ? "" : Encoding.UTF8.GetString(e.ApplicationMessage.Payload);
            if(Protocol.Level(topic,1)
        }
    }
}
