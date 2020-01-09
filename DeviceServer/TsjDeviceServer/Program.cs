using MQTTnet;
using MQTTnet.Client;
using System;
using System.Threading.Tasks;
using TsjDeviceServer.DeviceCtrl;

namespace TsjDeviceServer
{
    class Program
    {
        static void Main(string[] args)
        {
            CmdSrv cmdSrv = new CmdSrv();
            cmdSrv.Start();
            BackMqttClient backMqttClient = new BackMqttClient();
            Task.WaitAll( backMqttClient.Run(new MsgRecvHandler()));
            //Task.WaitAll( ClientTest.RunAsync());
            while(!Console.ReadLine().Equals("exit"));
            backMqttClient.Stop();
            cmdSrv.Stop();
        }
    }
}
