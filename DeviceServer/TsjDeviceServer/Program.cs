using MQTTnet;
using MQTTnet.Client;
using System;
using System.Threading.Tasks;

namespace TsjDeviceServer
{
    class Program
    {
        static void Main(string[] args)
        {
            BackMqttClient backMqttClient = new BackMqttClient();
            Task.WaitAll( backMqttClient.Run(new MsgRecvHandler()));
            //Task.WaitAll( ClientTest.RunAsync());
            while(!Console.ReadLine().Equals("exit"));
        }
    }
}
