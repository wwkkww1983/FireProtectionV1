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
            _pipeServer.Close();
        }

        private void ThreadCmdRecv(object obj)
        {
            _pipeServer.WaitForConnection();
            while (_bRun)
            {
                if (!_pipeServer.IsConnected)
                {
                    _pipeServer.Close();
                    _pipeServer = new NamedPipeServerStream("TsjCmdSrv", PipeDirection.In);
                    _pipeServer.WaitForConnection();//等待连接，程序会阻塞在此处，直到有一个连接到达
                }

                try
                {
                    using (StreamReader sr = new StreamReader(_pipeServer))
                    {
                        string cmdjson = sr.ReadLine();
                        if (string.IsNullOrEmpty(cmdjson))
                            continue;
                        Console.WriteLine(cmdjson);
                        ParseCmdJson(cmdjson);
                    }
                }
                catch (IOException e)
                {
                    Console.WriteLine("ERROR: {0}", e.Message);
                }
            }
        }

        private void ParseCmdJson(string cmdjson)
        {
            JObject jobj = JObject.Parse(cmdjson);
            var cmd = jobj["cmd"].ToString();
            if (cmd.Equals("Switch"))
            {
                var sn = jobj["deviceSn"].ToString();
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
            else if (cmd.Equals("ConfigPhase"))
            {
                var sn = jobj["deviceSn"].ToString();
                var phaseType = jobj["phaseType"].ToString();
                string jsonCmd = "";
                if (phaseType.Equals("单相"))
                {
                    var cmdData = new[]
                    {
                                    new
                                    {
                                        id="A",
                                        range_H=jobj["maxAmpere"].ToString(),
                                        range_L = jobj["minAmpere"].ToString(),
                                    },
                                    new
                                    {
                                        id="N",
                                        range_H=jobj["maxN"].ToString(),
                                        range_L = jobj["minN"].ToString(),
                                    },
                                    new
                                    {
                                        id="L",
                                        range_H=jobj["maxL"].ToString(),
                                        range_L = jobj["minL"].ToString(),
                                    }
                                };
                    jsonCmd = JsonConvert.SerializeObject(cmdData);
                }
                else
                {
                    var cmdData = new[]
                    {
                                    new
                                    {
                                        id="A",
                                        range_H=jobj["maxAmpere"].ToString(),
                                        range_L = jobj["minAmpere"].ToString(),
                                    },
                                    new
                                    {
                                        id="N",
                                        range_H=jobj["maxN"].ToString(),
                                        range_L = jobj["minN"].ToString(),
                                    },
                                    new
                                    {
                                        id="L1",
                                        range_H=jobj["maxL1"].ToString(),
                                        range_L = jobj["minL1"].ToString(),
                                    },
                                    new
                                    {
                                        id="L2",
                                        range_H=jobj["maxL2"].ToString(),
                                        range_L = jobj["minL2"].ToString(),
                                    },
                                    new
                                    {
                                        id="L3",
                                        range_H=jobj["maxL3"].ToString(),
                                        range_L = jobj["minL3"].ToString(),
                                    }
                                };
                    jsonCmd = JsonConvert.SerializeObject(cmdData);
                }
                var applicationMessage = new MqttApplicationMessageBuilder()
                .WithTopic($"Set/{sn}/Config")
                .WithPayload(jsonCmd)
                .WithAtLeastOnceQoS()
                .Build();
                BackMqttClient.Send(applicationMessage);
            }
        }
    }
}
