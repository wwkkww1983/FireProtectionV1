using Common;
using FireProtectionV1.TsjDevice.Dto;
using MQTTnet;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using TsjWebApi;

namespace TsjDeviceServer
{
    class CheckUpdateFirmware
    {
        Thread _thread;
        bool _bRun = false;

        private void CheckThread(object obj)
        {
            while (_bRun)
            {
                try
                {
                    GetUpdateFireware();
                }
                catch (Exception e)
                {
                    Logger.Log.Error(e.Message);
                }
                Thread.Sleep(30000);
            }
        }

        private void GetUpdateFireware()
        {
            var res=FireApi.HttpGetTsj(Config.Url("/api/services/app/TsjDevice/GetUpdateFirmwareList"));
            var lstUpdate = JsonConvert.DeserializeObject<List<UpdateFirmwareOutput>>(res);
            foreach(var v in lstUpdate)
            {
                var data = new
                {
                    url = v.Url
                };
                var applicationMessage = new MqttApplicationMessageBuilder()
                    .WithTopic($"Set/{v.Sn}/Firmware")
                    .WithPayload(JsonConvert.SerializeObject(data))
                    .WithAtLeastOnceQoS()
                    .Build();
                BackMqttClient.Send(applicationMessage);
            }
        }
        public void Start()
        {
            _bRun = true;
            _thread = new Thread(CheckThread);
            _thread.Start();
        }
        public void Stop()
        {
            _bRun = false;
            _thread.Join();
        }
    }
}
