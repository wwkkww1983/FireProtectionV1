using FireProtectionV1.AppService;
using FireProtectionV1.FireWorking.Dto;
using FireProtectionV1.FireWorking.Manager;
using FireProtectionV1.SettingCore.Manager;
using FireProtectionV1.SettingCore.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FireProtectionV1.DeviceService
{
    public class DataAppService : AppServiceBase
    {
        IPatrolManager _patrolManager;
        IDutyManager _dutyManager;
        IFireSettingManager _fireSettingManager;
        IDeviceManager _deviceManager;
        IAlarmManager _alarmManager;
        IFaultManager _faultManager;
        public DataAppService(
            IPatrolManager patrolManager,
            IDutyManager dutyManager,
            IFaultManager faultManager,
            IFireSettingManager fireSettingManager,
            IDeviceManager deviceManager,IAlarmManager alarmManager)
        {
            _patrolManager = patrolManager;
            _dutyManager = dutyManager;
            _faultManager = faultManager;
            _fireSettingManager = fireSettingManager;
            _deviceManager = deviceManager;
            _alarmManager = alarmManager;
        }
        /// <summary>
        /// 新增安全用电数据温度(超限判断后新增报警)
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<AddDataOutput> AddDataElecT(AddDataElecInput input)
        {
            //await _deviceManager.AddRecordAnalog(input);
            var setting =await _fireSettingManager.GetByName("CableTemperature");
            Console.WriteLine($"{DateTime.Now} 收到模拟量值 AddDataElecT Analog:{input.Analog}{input.Unit} 部件地址：{input.Identify} 网关地址：{input.GatewayIdentify}");
            if (input.Analog >= setting.MaxValue)
            {
                Console.WriteLine($"{DateTime.Now} 触发报警 Analog:{input.Analog}{input.Unit} 报警限值:{setting.MaxValue} 部件地址：{input.Identify} 网关地址：{input.GatewayIdentify}");
                return await _alarmManager.AddAlarmElec(input,$"{setting.MaxValue}{input.Unit}");
            }
            return await Task.FromResult<AddDataOutput>(new AddDataOutput() { IsDetectorExit = true });
        }
        /// <summary>
        /// 新增安全用电数据电流(超限判断后新增报警)
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<AddDataOutput> AddDataElecE(AddDataElecInput input)
        {
            //await _deviceManager.AddRecordAnalog(input);
            var setting = await _fireSettingManager.GetByName("ResidualCurrent");
            Console.WriteLine($"{DateTime.Now} 收到模拟量值 AddDataElecE Analog:{input.Analog}{input.Unit} 部件地址：{input.Identify} 网关地址：{input.GatewayIdentify}");
            if (input.Analog >= setting.MaxValue)
            {
                Console.WriteLine($"{DateTime.Now} 触发报警 Analog:{input.Analog}{input.Unit} 报警限值:{setting.MaxValue} 部件地址：{input.Identify} 网关地址：{input.GatewayIdentify}");
                return await _alarmManager.AddAlarmElec(input, $"{setting.MaxValue}{input.Unit}");
            }
            return await Task.FromResult<AddDataOutput>(new AddDataOutput() { IsDetectorExit = true });
        }
        /// <summary>
        /// 新增火灾监控设备报警
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<AddDataOutput> AddAlarmFire(AddAlarmFireInput input)
        {
            Console.WriteLine($"{DateTime.Now} 收到报警 AddAlarmFire 部件类型:{input.DetectorGBType.ToString()} 部件地址：{input.Identify} 网关地址：{input.GatewayIdentify}");
            return await _alarmManager.AddAlarmFire(input);
        }
        //public void Test()
        //{
        //    for(int fireunitid=5; fireunitid<20; fireunitid++)
        //    {
        //        _deviceManager.AddGateway(new AddGatewayInput()
        //        {
        //            FireSysType = 1,
        //            FireUnitId = fireunitid,
        //            Identify = "66." + fireunitid,
        //            Location = "",
        //            Origin = ""
        //        });
        //        _deviceManager.AddGateway(new AddGatewayInput()
        //        {
        //            FireSysType = 2,
        //            FireUnitId = fireunitid,
        //            Identify = "88." + fireunitid,
        //            Location = "",
        //            Origin = ""
        //        });
        //    }
        //}
        //public void TestDetector()
        //{
        //    for (int fireunitid = 5; fireunitid < 20; fireunitid++)
        //    {
        //        _deviceManager.AddDetector(new AddDetectorInput()
        //        {
        //            DetectorGBType = 17,
        //            GatewayIdentify = "66." + fireunitid,
        //            Identify = fireunitid + ".0.0.1",
        //            Location = $"{new Random().Next(1, 3)}楼配电室",
        //            Origin = ""
        //        });
        //        _deviceManager.AddDetector(new AddDetectorInput()
        //        {
        //            DetectorGBType = 18,
        //            GatewayIdentify = "66." + fireunitid,
        //            Identify = fireunitid + ".0.0.2",
        //            Location = $"{new Random().Next(1, 3)}楼配电室",
        //            Origin = ""
        //        });
        //        _deviceManager.AddDetector(new AddDetectorInput()
        //        {
        //            DetectorGBType = 23,
        //            GatewayIdentify = "88." + fireunitid,
        //            Identify = fireunitid + ".2.0.1",
        //            Location = $"{new Random().Next(1, 3)}楼{new Random().Next(1, 20)}号",
        //            Origin = ""
        //        });
        //        _deviceManager.AddDetector(new AddDetectorInput()
        //        {
        //            DetectorGBType = 40,
        //            GatewayIdentify = "88." + fireunitid,
        //            Identify = fireunitid + ".2.0.2",
        //            Location = $"{new Random().Next(1, 3)}楼{new Random().Next(1, 20)}号",
        //            Origin = ""
        //        });
        //        _deviceManager.AddDetector(new AddDetectorInput()
        //        {
        //            DetectorGBType = 69,
        //            GatewayIdentify = "88." + fireunitid,
        //            Identify = fireunitid + ".2.0.3",
        //            Location = $"{new Random().Next(1, 3)}楼{new Random().Next(1, 20)}号",
        //            Origin = ""
        //        });
        //    }
        //}
        // void TestAlarmXinYuan()
        //{
        //    for (int i = 0; i < 20; i++)
        //    {
        //        _alarmManager.AddAlarmElec(new AddDataElecInput()
        //        {
        //            Analog = new Random().Next(101, 299),
        //            DetectorGBType = 18,
        //            GatewayIdentify = "81.31.0.0.0.11",
        //            Identify = "1.0.0.0",
        //            Unit = "℃",
        //            Origin = "安吉斯"
        //        }, "60℃");
        //    }
        //    for (int i = 0; i < 20; i++)
        //    {
        //        _alarmManager.AddAlarmFire(new AddAlarmFireInput()
        //        {
        //            DetectorGBType = 23,
        //            GatewayIdentify = "11.0.0.1.200.1",
        //            Identify = "3.0.1.0",
        //            Origin = "安吉斯"
        //        });
        //        _alarmManager.AddAlarmFire(new AddAlarmFireInput()
        //        {
        //            DetectorGBType = 2,
        //            GatewayIdentify = "11.0.0.1.200.1",
        //            Identify = "11.0.0.1.200.1",
        //            Origin = "安吉斯"
        //        });
        //    }
        //}
        // void TestAlarm()
        //{
        //    for (int fireunitid = 5; fireunitid < 20; fireunitid++)
        //    {
        //        int n = new Random().Next(1, 10);
        //        for (int i = 0; i < n; i++)
        //        {
        //            _alarmManager.AddAlarmElec(new AddDataElecInput()
        //            {
        //                Analog = new Random().Next(101, 1999),
        //                DetectorGBType = 17,
        //                GatewayIdentify = "66." + fireunitid,
        //                Identify = fireunitid + ".0.0.1",
        //                Unit = "A",
        //                Origin = ""
        //            }, "100A");
        //        }
        //        n = new Random().Next(1, 10);
        //        for (int i = 0; i < n; i++)
        //        {
        //            _alarmManager.AddAlarmElec(new AddDataElecInput()
        //            {
        //                Analog = new Random().Next(101, 299),
        //                DetectorGBType = 18,
        //                GatewayIdentify = "66." + fireunitid,
        //                Identify = fireunitid + ".0.0.2",
        //                Unit = "℃",
        //                Origin = ""
        //            }, "60℃");
        //        }
        //        n = new Random().Next(1, 10);
        //        for (int i = 0; i < n; i++)
        //        {
        //            _alarmManager.AddAlarmFire(new AddAlarmFireInput()
        //            {
        //                DetectorGBType = 23,
        //                GatewayIdentify = "88." + fireunitid,
        //                Identify = fireunitid + ".2.0.1",
        //                Origin = ""
        //            });
        //        }
        //        n = new Random().Next(1, 10);
        //        for (int i = 0; i < n; i++)
        //        {
        //            _alarmManager.AddAlarmFire(new AddAlarmFireInput()
        //            {
        //                DetectorGBType = 40,
        //                GatewayIdentify = "88." + fireunitid,
        //                Identify = fireunitid + ".2.0.2",
        //                Origin = ""
        //            });
        //        }
        //        n = new Random().Next(1, 10);
        //        for (int i = 0; i < n; i++)
        //        {
        //            _alarmManager.AddAlarmFire(new AddAlarmFireInput()
        //            {
        //                DetectorGBType = 69,
        //                GatewayIdentify = "88." + fireunitid,
        //                Identify = fireunitid + ".2.0.3",
        //                Origin = ""
        //            });
        //        }
        //    }
        //}
        // void TestFaultXy()
        //{
        //    for (int i = 0; i < 5; i++)
        //    {
        //        _faultManager.AddNewFault(new AddNewFaultInput()
        //        {
        //            DetectorGBType = 18,
        //            FaultRemark = "温感失效",
        //            GatewayIdentify = "81.31.0.0.0.11",
        //            Identify = "1.0.0.0",
        //            Origin = "安吉斯"
        //        });
        //    }
        //    for (int i = 0; i < 20; i++)
        //    {
        //        _faultManager.AddNewFault(new AddNewFaultInput()
        //        {
        //            DetectorGBType = 2,
        //            FaultRemark = "通信故障",
        //            GatewayIdentify = "11.0.0.1.200.1",
        //            Identify = "11.0.0.1.200.1",
        //            Origin = "安吉斯"
        //        });
        //    }
        //}
        //public void TestFault()
        //{
        //    for (int fireunitid = 5; fireunitid < 20; fireunitid++)
        //    {
        //        int n = new Random().Next(1, 5);
        //        for (int i = 0; i < n; i++)
        //        {
        //            _faultManager.AddNewFault(new AddNewFaultInput()
        //            {
        //                DetectorGBType = 17,
        //                FaultRemark="线路破损",
        //                GatewayIdentify = "66." + fireunitid,
        //                Identify = fireunitid + ".0.0.1",
        //                Origin = ""
        //            });
        //        }
        //        n = new Random().Next(1, 5);
        //        for (int i = 0; i < n; i++)
        //        {
        //            _faultManager.AddNewFault(new AddNewFaultInput()
        //            {
        //                DetectorGBType = 18,
        //                FaultRemark = "温感失效",
        //                GatewayIdentify = "66." + fireunitid,
        //                Identify = fireunitid + ".0.0.2",
        //                Origin = ""
        //            });
        //        }
        //        n = new Random().Next(1, 5);
        //        for (int i = 0; i < n; i++)
        //        {
        //            _faultManager.AddNewFault(new AddNewFaultInput()
        //            {
        //                DetectorGBType = 23,
        //                FaultRemark = "按钮损坏",
        //                GatewayIdentify = "88." + fireunitid,
        //                Identify = fireunitid + ".2.0.1",
        //                Origin = ""
        //            });
        //        }
        //        n = new Random().Next(1, 5);
        //        for (int i = 0; i < n; i++)
        //        {
        //            _faultManager.AddNewFault(new AddNewFaultInput()
        //            {
        //                DetectorGBType = 40,
        //                FaultRemark = "烟感失灵",
        //                GatewayIdentify = "88." + fireunitid,
        //                Identify = fireunitid + ".2.0.2",
        //                Origin = ""
        //            });
        //        }
        //        n = new Random().Next(1, 5);
        //        for (int i = 0; i < n; i++)
        //        {
        //            _faultManager.AddNewFault(new AddNewFaultInput()
        //            {
        //                DetectorGBType = 69,
        //                FaultRemark = "感光异常",
        //                GatewayIdentify = "88." + fireunitid,
        //                Identify = fireunitid + ".2.0.3",
        //                Origin = ""
        //            });
        //        }
        //    }
        //}
        //void TestPatrolDuty()
        //{
        //    for (int fireunitid = 1; fireunitid <= 145; fireunitid++)
        //    {
        //        int n = new Random().Next(1, 10);
        //        for (int i = 0; i < n; i++)
        //        {
        //            _dutyManager.AddNewDuty(new AddNewDutyInput()
        //            {
        //                DutyStatus = 1,
        //                DutyPicture = "",
        //                FireUnitId = fireunitid,
        //                FireUnitUserId = 1
        //            });
        //        }
        //         n = new Random().Next(1, 10);
        //        for (int i = 0; i < n; i++)
        //        {
        //            _patrolManager.AddNewPatrol(new AddNewPatrolInput()
        //            {
        //                FireUnitId = fireunitid,
        //                FireUnitUserId = 1
        //            });
        //        }
        //    }
        //}
        /// <summary>
        /// 生成当天模拟数据
        /// </summary>
        public void GetSimulateTodayData()
        {
            int num= new Random().Next(1, 5);
            List<int> lstMis = new List<int>();
            for(int i = 0; i < num; i++)
            {
                lstMis.Add(new Random().Next(1, 145));
            }
            for (int fireunitid = 1; fireunitid <= 145; fireunitid++)
            {
                if (lstMis.Find(p => p == fireunitid) != 0)
                    continue;
                _dutyManager.AddNewDuty(new AddNewDutyInput()
                {
                    DutyStatus = 1,
                    DutyPicture = "",
                    FireUnitId = fireunitid,
                    FireUnitUserId = 1
                });
                if (DateTime.Now.DayOfWeek == DayOfWeek.Friday)//周五巡查
                {
                    _patrolManager.AddNewPatrol(new AddNewPatrolInput()
                    {
                        FireUnitId = fireunitid,
                        FireUnitUserId = 1
                    });
                }
                //只有5到20的防火单位id配置了设备
                if(fireunitid>=5 && fireunitid < 20)
                {
                    if(new Random().Next(1, 5) == 1)
                    {
                        _alarmManager.AddAlarmElec(new AddDataElecInput()
                        {
                            Analog = new Random().Next(101, 1999),
                            DetectorGBType = 17,
                            GatewayIdentify = "66." + fireunitid,
                            Identify = fireunitid + ".0.0.1",
                            Unit = "A",
                            Origin = ""
                        }, "100A");
                        _alarmManager.AddAlarmElec(new AddDataElecInput()
                        {
                            Analog = new Random().Next(101, 299),
                            DetectorGBType = 18,
                            GatewayIdentify = "66." + fireunitid,
                            Identify = fireunitid + ".0.0.2",
                            Unit = "℃",
                            Origin = ""
                        }, "60℃");
                        _alarmManager.AddAlarmFire(new AddAlarmFireInput()
                        {
                            DetectorGBType = 23,
                            GatewayIdentify = "88." + fireunitid,
                            Identify = fireunitid + ".2.0.1",
                            Origin = ""
                        });
                        _alarmManager.AddAlarmFire(new AddAlarmFireInput()
                        {
                            DetectorGBType = 40,
                            GatewayIdentify = "88." + fireunitid,
                            Identify = fireunitid + ".2.0.2",
                            Origin = ""
                        });
                        _alarmManager.AddAlarmFire(new AddAlarmFireInput()
                        {
                            DetectorGBType = 69,
                            GatewayIdentify = "88." + fireunitid,
                            Identify = fireunitid + ".2.0.3",
                            Origin = ""
                        });
                    }
                }
            }
            //兴源大厦的设备
            if (new Random().Next(1, 5) == 1)
            {
                _faultManager.AddNewFault(new AddNewFaultInput()
                {
                    DetectorGBType = 18,
                    FaultRemark = "温感失效",
                    GatewayIdentify = "81.31.0.0.0.11",
                    Identify = "1.0.0.0",
                    Origin = "安吉斯"
                });
                _faultManager.AddNewFault(new AddNewFaultInput()
                {
                    DetectorGBType = 2,
                    FaultRemark = "通信故障",
                    GatewayIdentify = "11.0.0.1.200.1",
                    Identify = "11.0.0.1.200.1",
                    Origin = "安吉斯"
                });

            }
        }
    }
}
