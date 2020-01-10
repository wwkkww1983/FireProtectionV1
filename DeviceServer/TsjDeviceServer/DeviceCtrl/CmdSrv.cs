using MQTTnet;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Pipes;
using System.Text;
using System.Threading;
using TsjDeviceServer.Data;

namespace TsjDeviceServer.DeviceCtrl
{
    class CmdSrv
    {
        NamedPipeServerStream _pipeServer;
        Thread _thread;
        bool _bRun=false;
        public CmdSrv()
        {
            _pipeServer = new NamedPipeServerStream("TsjCmdSrv", PipeDirection.In);
        }
        public void Start()
        {
            _bRun = true;
            _thread = new Thread(ThreadCmdRecv);
            _thread.Start();
        }
        public void Stop()
        {
            _pipeServer.Disconnect();
            _pipeServer.Close();
        }

        private void ThreadCmdRecv(object obj)
        {
            while (_bRun)
            {
                _pipeServer.WaitForConnection();//等待连接，程序会阻塞在此处，直到有一个连接到达
                try
                {
                    using (StreamReader sr = new StreamReader(_pipeServer))
                    {
                        JObject jobj=JObject.Parse(sr.ReadLine());
                        var cmd = jobj["cmd"].ToString();
                        if (cmd.Equals("Switch"))
                        {
                            var sn= jobj["deviceSn"].ToString();
                            DataSwitch dataSwitch = new DataSwitch()
                            {
                                at = TsjConvert.ToUnixTimestamp(DateTime.Now),
                                state = "ON"
                            };
                            var applicationMessage = new MqttApplicationMessageBuilder()
                            .WithTopic($"Set/{sn}/Switch")
                            .WithPayload(JsonConvert.SerializeObject(dataSwitch))
                            .WithAtLeastOnceQoS()
                            .Build();

                            BackMqttClient.Send(applicationMessage);
                        }
                        else if (cmd.Equals("UpdateAnalog"))
                        {
                            var sn = jobj["deviceSn"].ToString();
                            var cmdData = new
                            {
                                at = TsjConvert.ToUnixTimestamp(DateTime.Now)
                            };
                            var applicationMessage = new MqttApplicationMessageBuilder()
                            .WithTopic($"Set/{sn}/UpdateMonitor")
                            .WithPayload(JsonConvert.SerializeObject(cmdData))
                            .WithAtLeastOnceQoS()
                            .Build();

                            BackMqttClient.Send(applicationMessage);
                        }
                    }
                }
                catch (IOException e)
                {
                    Console.WriteLine("ERROR: {0}", e.Message);
                }
            }
        }
    }
}
