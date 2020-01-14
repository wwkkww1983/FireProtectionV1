using Common;
using DeviceServer.Tcp;
using DeviceServer.Tcp.Protocol;
using DeviceServer.Tcp.Protocol.Datas;
using FireProtectionV1.FireWorking.Dto;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using TsjWebApi;

namespace DeviceServer
{
    class Disposal
    {
        object _lock = new object();
        Dictionary<string, DateTime> _lastTimeIdentifys = new Dictionary<string, DateTime>();
        //Transfer _transfer;
        Thread _tCheckOnline;
        bool _bRun = true;
        public void StartOnlineCheck()
        {
            _tCheckOnline = new Thread(OfflineFunc);
            _tCheckOnline.Start();
        }
        public void StopAllThread()
        {
            _bRun = false;
            _tCheckOnline.Join();
        }
        private void OfflineFunc(object obj)
        {
            while (_bRun)
            {
                Thread.Sleep(3000 * 60);
                lock (_lock)
                {
                    List<string> keys = new List<string>();
                    foreach (var v in _lastTimeIdentifys)
                    {
                        if (DateTime.Now - v.Value > new TimeSpan(0, 1, 0))
                        {
                            FireApi.HttpPost(Config.Url("/api/services/app/Data/AddOnlineGateway"), new AddOnlineGatewayInput()
                            {
                                Identify = v.Key,
                                Origin = "安吉斯",
                                IsOnline = false
                            });
                            keys.Add(v.Key);
                            //var ss = v.Key.Split(',');
                            //if (ss.Length == 1)
                            //    FireApi.HttpPost("/api/services/app/Data/AddOnlineGateway", new AddOnlineGatewayInput() {
                            //        Identify = ss[0],
                            //        Origin = "安吉斯",
                            //        IsOnline = false
                            //    });
                            //else if(ss.Length==2)
                            //    CheckDetectorExitToPost("/api/services/app/Data/AddOnlineDetector", new AddOnlineDetectorInput()
                            //    {
                            //        Identify = ss[1],
                            //        Origin = "安吉斯",
                            //        IsOnline = false,
                            //        GatewayIdentify=ss[0]
                            //    });
                        }
                    }
                    foreach (var key in keys)
                    {
                        _lastTimeIdentifys.Remove(key);
                    }
                }
            }
        }

        //public Disposal(Transfer transfer = null)
        //{
        //    _transfer = transfer;
        //}
        internal void OnData(Session session, Packet pack)
        {
            Console.WriteLine($"OnData pack.Cmd{pack.Cmd}");
            //OnlineGateway(pack.SrcAddressString);
            //转发
            //_transfer?.Send(pack.SrcAddressString, pack.Data.ToArray(), session);
            //解析
            if (pack.Cmd == Cmd.Send)
            {
                DataPacket dp = new DataPacket(pack.DataUnit);
                //if (!Enum.IsDefined(typeof(DataType), pack.DataUnit[0]))
                //    return;
                //DataType dataType = (DataType)pack.DataUnit[0];
                Console.WriteLine($"dp.DataType{dp.DataType}");
                switch (dp.DataType)
                {
                    case DataType.UploadUnitAnalog://上传模拟量值
                        {
                            DataUploadAnalog data = (DataUploadAnalog)dp.Datas[0];
                            //OnlineDetector(pack.SrcAddressString,data.UnitAddressString);
                            //var param = new AddDataElecInput
                            //{
                            //    DetectorGBType = data.UnitType,
                            //    Identify = data.UnitAddressString,
                            //    GatewayIdentify = pack.SrcAddressString,
                            //    Analog = data.Analog,
                            //    Unit = data.AnalogUnit
                            //};
                            Console.WriteLine($"{DateTime.Now} 收到模拟量 Analog：{data.Analog}{data.AnalogUnit} 部件地址：{data.UnitAddressString} 网关地址：{pack.SrcAddressString}");
                            //if (data.AnalogType == AnalogType.Temperature)
                            //    CheckDetectorExitToPost("/api/services/app/Data/AddDataElecT", param);
                            //else if (data.AnalogType == AnalogType.ResidualAjs)
                            //    CheckDetectorExitToPost("/api/services/app/Data/AddDataElecE", param);
                            if(data.AnalogType == AnalogType.Temperature|| data.AnalogType == AnalogType.ResidualAjs)
                            FireApi.HttpPostTsj(Config.Url("/api/services/app/FireDevice/AddElecRecord"), new 
                            {
                                fireElectricDeviceSn = pack.SrcAddressString,
                                sign = data.UnitAddressString,
                                analog = data.Analog
                            });
                        }
                        break;
                    case DataType.AjsA1:
                    case DataType.UploadUITDTime://上传用户信息装置时间
                        //DataUploadTime data = new DataUploadTime(pack.DataUnit.GetRange(2, 6));
                        //同步时间
                        Console.WriteLine("收到上传用户信息装置时间");
                        session.SendPacket(pack, Cmd.Control, DataType.SyncUITDTime, new List<DataObject>() { new DataSyncTime() });
                        break;
                    case DataType.UploadUITDOperatInfo:
                        {
                            DataUITDOperatInfo data = (DataUITDOperatInfo)dp.Datas[0];
                            if (data.AlarmManual())
                            {
                                //报警
                                Console.WriteLine($"{DateTime.Now} 用户信息传输装置手动报警");
                                var param = new
                                {
                                    detectorSn = pack.SrcAddressString,
                                    fireAlarmDeviceSn = pack.SrcAddressString,
                                };
                                FireApi.HttpPostTsj(Config.Url("/api/services/app/Alarm/AddAlarmFire"), param);
                            }
                        }
                        break;
                    case DataType.UploadUnitState:
                        {
                            DataUnitState data = (DataUnitState)dp.Datas[0];
                            if (data.Alarm()
                                && data.UnitType != (byte)UnitType.ElectricTemperature
                                && data.UnitType != (byte)UnitType.ElectricResidual)
                            {
                                //报警
                                Console.WriteLine($"{DateTime.Now} 部件报警，部件地址：{data.UnitAddressString} 网关地址：{pack.SrcAddressString}");
                                var param = new 
                                {
                                    detectorSn = data.UnitAddressString,
                                    fireAlarmDeviceSn = pack.SrcAddressString,
                                };
                                FireApi.HttpPostTsj(Config.Url("/api/services/app/Alarm/AddAlarmFire"), param);
                            }
                        }
                        break;
                }
            }
            else if (pack.Cmd == Cmd.Ajs08)
            {
                //同步时间
                session.SendPacket(pack, Cmd.Control, DataType.SyncUITDTime, new List<DataObject>() { new DataSyncTime() });
            }

        }

        //private void OnlineDetector(string srcAddressString, string unitAddressString)
        //{
        //    lock (_lock)
        //    {
        //        string key = $"{srcAddressString},{unitAddressString}";
        //        if (!_lastTimeIdentifys.ContainsKey(key))
        //        {
        //            CheckDetectorExitToPost("/api/services/app/Data/AddOnlineDetector", new AddOnlineDetectorInput()
        //            {
        //                Identify = unitAddressString,
        //                IsOnline = true,
        //                Origin = "安吉斯",
        //                GatewayIdentify=srcAddressString
        //            });
        //        }
        //        _lastTimeIdentifys[key] = DateTime.Now;

        //    }
        //}

        /// <summary>
        /// 新增 网关在线离线事件
        /// </summary>
        /// <param name="srcAddressString"></param>
        private void OnlineGateway(string srcAddressString)
        {
            lock (_lock)
            {
                if (!_lastTimeIdentifys.ContainsKey(srcAddressString))
                {
                    CheckDetectorExitToPost("/api/services/app/Data/AddOnlineGateway", new AddOnlineGatewayInput()
                    {
                        Identify = srcAddressString,
                        IsOnline = true,
                        Origin = "安吉斯"
                    });
                }
                _lastTimeIdentifys[srcAddressString] = DateTime.Now;

            }
        }

        private void CheckDetectorExitToPost(string urlapi, DeviceBaseInput param)
        {
            string res = FireApi.HttpPost(Config.Url(urlapi), param);
            string postData = JsonConvert.SerializeObject(param);
            if (string.IsNullOrEmpty(res))
                return;
            var jobj = JObject.Parse(res);
            var IsDetectorExit = jobj["isDetectorExit"].ToString();
            if (IsDetectorExit.Equals("False"))
            {
                var p = JObject.Parse(postData);
                var paramadd = new AddDetectorInput
                {
                    DetectorGBType = byte.Parse(p["DetectorGBType"].ToString()),
                    Identify = p["Identify"].ToString(),
                    GatewayIdentify = p["GatewayIdentify"].ToString(),
                    Location = "",
                };
                FireApi.HttpPost(Config.Url("/api/services/app/Device/AddDetector") , paramadd);
                FireApi.HttpPost(Config.Url(urlapi), param);
            }
        }
    }
}
