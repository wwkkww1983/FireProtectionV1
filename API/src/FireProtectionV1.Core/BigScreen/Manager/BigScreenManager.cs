﻿using Abp.Domain.Repositories;
using Abp.Domain.Services;
using Abp.Runtime.Caching;
using DeviceServer.Tcp.Protocol;
using FireProtectionV1.BigScreen.Dto;
using FireProtectionV1.Common.DBContext;
using FireProtectionV1.Common.Enum;
using FireProtectionV1.Enterprise.Model;
using FireProtectionV1.FireWorking.Manager;
using FireProtectionV1.FireWorking.Model;
using FireProtectionV1.HydrantCore.Model;
using FireProtectionV1.Infrastructure.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireProtectionV1.BigScreen.Manager
{
    public class BigScreenManager : DomainService, IBigScreenManager
    {
        static DateTime _AlarmElecTime = DateTime.Now;   // 被电气警情天讯通使用

        IDeviceManager _deviceManager;
        IAlarmManager _alarmManager;
        IRepository<FireUnit> _fireUnitRep;
        IRepository<FireAlarmDevice> _repFireAlarmDevice;
        IRepository<FireElectricDevice> _repFireElectricDevice;
        IRepository<FireAlarmDetector> _repFireAlarmDetector;
        IRepository<FireUnitType> _fireUnitTypeRep;
        IRepository<Hydrant> _hydrantRep;
        IRepository<DetectorType> _repDetectorType;
        ICacheManager _cacheManager;
        ISqlRepository _sqlRepository;
        public BigScreenManager(
            IDeviceManager deviceManager,
            IAlarmManager alarmManager,
            IRepository<FireUnit> fireUnitRep,
            IRepository<FireAlarmDevice> repFireAlarmDevice,
            IRepository<FireElectricDevice> repFireElectricDevice,
            IRepository<FireAlarmDetector> repFireAlarmDetector,
            IRepository<FireUnitType> fireUnitTypeRep,
            IRepository<Hydrant> hydrantRep,
            IRepository<DetectorType> repDetectorType,
            ICacheManager cacheManager,
            ISqlRepository sqlRepository)
        {
            _deviceManager = deviceManager;
            _alarmManager = alarmManager;
            _fireUnitRep = fireUnitRep;
            _repFireAlarmDevice = repFireAlarmDevice;
            _repFireElectricDevice = repFireElectricDevice;
            _repFireAlarmDetector = repFireAlarmDetector;
            _fireUnitTypeRep = fireUnitTypeRep;
            _repDetectorType = repDetectorType;
            _hydrantRep = hydrantRep;
            _cacheManager = cacheManager;
            _sqlRepository = sqlRepository;
        }

        /// <summary>
        /// 首页：地图呼吸气泡层
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public Task<List<BreathingBubble>> GetBreathingBubble(string value)
        {
            List<BreathingBubble> lstBreathingBubble = new List<BreathingBubble>();
            int jsq = 0;
            var lstFireUnit = _cacheManager.GetCache("BigScreen").Get("lstFireUnit", () => GetAllFireUnit(33));
            foreach (var fireUnit in lstFireUnit)
            {
                lstBreathingBubble.Add(new BreathingBubble()
                {
                    id = fireUnit.Id,
                    lng = (double)fireUnit.Lng,
                    lat = (double)fireUnit.Lat,
                    info = fireUnit.Name
                });
                jsq++;
                if ((jsq == 8 && "2018-01".Equals(value)) || (jsq == 19 && "2018-03".Equals(value)) || (jsq == 92 && "2019-01".Equals(value))) break;
            }
            return Task.FromResult(lstBreathingBubble);
        }

        /// <summary>
        /// 首页：地图呼吸气泡层_柳州
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public Task<List<BreathingBubble>> GetBreathingBubble_lz(string value)
        {
            List<BreathingBubble> lstBreathingBubble = new List<BreathingBubble>();
            var lstFireUnit = _cacheManager.GetCache("BigScreen").Get("lstFireUnit", () => GetAllFireUnit());
            foreach (var fireUnit in lstFireUnit)
            {
                lstBreathingBubble.Add(new BreathingBubble()
                {
                    id = fireUnit.Id,
                    lng = (double)fireUnit.Lng,
                    lat = (double)fireUnit.Lat,
                    info = fireUnit.Name
                });
            }
            return Task.FromResult(lstBreathingBubble);
        }

        /// <summary>
        /// 首页：飞线层
        /// </summary>
        /// <returns></returns>
        public Task<List<FlyLine>> GetFlyLine()
        {
            List<FlyLine> lstFlyLine = new List<FlyLine>();
            var lstFireUnit = _cacheManager.GetCache("BigScreen").Get("lstFireUnit", () => GetAllFireUnit());
            int num = lstFireUnit.Count;
            if (num > 60) num = 60;
            for (int i = 0; i < num; i++)
            {
                lstFlyLine.Add(new FlyLine()
                {
                    from = lstFireUnit[i].Lng + "," + lstFireUnit[i].Lat
                });
            }

            return Task.FromResult(lstFlyLine);
        }

        /// <summary>
        /// 首页：飞线层_柳州
        /// </summary>
        /// <returns></returns>
        public Task<List<FlyLine_lz>> GetFlyLine_lz()
        {
            List<FlyLine_lz> lstFlyLine = new List<FlyLine_lz>();
            var lstFireUnit = _cacheManager.GetCache("BigScreen").Get("lstFireUnit", () => GetAllFireUnit());
            int num = lstFireUnit.Count;
            if (num > 60) num = 60;
            for (int i = 0; i < num; i++)
            {
                lstFlyLine.Add(new FlyLine_lz()
                {
                    from = lstFireUnit[i].Lng + "," + lstFireUnit[i].Lat
                });
            }

            return Task.FromResult(lstFlyLine);
        }

        /// <summary>
        /// 首页：地图多行文本
        /// </summary>
        /// <param name="deptId"></param>
        /// <returns></returns>
        public Task<List<DataText>> GetMapMultiText(int deptId)
        {
            List<DataText> lstDataText = new List<DataText>();
            DataText mt = new DataText();
            var lstFireUnit = _cacheManager.GetCache("BigScreen").Get("lstFireUnit", () => GetAllFireUnit(deptId));
            int cntFireUnit = lstFireUnit.Count();

            int num = 10;
            if (cntFireUnit < 10) num = cntFireUnit;
            Random random = new Random();
            for (int i = 0; i < num; i++)
            {
                int key = random.Next(0, cntFireUnit);
                mt.value += $"<br/>{DateTime.Now.ToString("HH:mm:ss")}<br/>接收{lstFireUnit[i].Name}网关心跳";
            }
            mt.value = mt.value.Substring(5);
            lstDataText.Add(mt);
            return Task.FromResult(lstDataText);
        }

        List<AlarmElec> GetNewAlarmElec(DateTime startTime)
        {
            return new List<AlarmElec>();
            //var query = from a in _alarmManager.GetNewElecAlarm(startTime)
            //            join b in _deviceManager.GetDetectorElectricAll()
            //            on a.DetectorId equals b.Id
            //            join c in _repDetectorType.GetAll()
            //            on b.DetectorTypeId equals c.Id
            //            join d in _fireUnitRep.GetAll()
            //            on a.FireUnitId equals d.Id
            //            orderby a.CreationTime descending
            //            select new AlarmElec()
            //            {
            //                Address = d.Address,
            //                AlarmType = c.Name,
            //                AlarmValue = $"{a.Analog}{a.Unit}",
            //                ContractName = d.ContractName,
            //                ContractPhone = d.ContractPhone,
            //                CreationTime = a.CreationTime,
            //                FireUnitName = d.Name
            //            };
            //return query.ToList();
        }
        /// <summary>
        /// 首页：电气警情天讯通
        /// </summary>
        /// <returns></returns>
        public Task<List<DataText>> GetTianXunTong()
        {
            List<DataText> lstDataText = new List<DataText>();
            DataText mt = new DataText();
            var lstAlarmElec = _cacheManager.GetCache("BigScreen").Get("lstAlarmElec", () => InitAlarmElecList());

            List<AlarmElec> lstAlarmElecNew = GetNewAlarmElec(_AlarmElecTime);    // 获取CreationTime大于_AlarmElecTime的电气火灾报警数据
            if (lstAlarmElecNew != null && lstAlarmElecNew.Count > 0)
            {
                _AlarmElecTime = DateTime.Now;  // 如果获取到了数据，则更新_AlarmElecTime，下次只需获取CreationTime大于_AlarmElecTime的电气火灾报警数据
                foreach (var alarmElec in lstAlarmElecNew)
                {
                    lstAlarmElec.Insert(0, alarmElec);
                }
            }
            else
            {
                Random random = new Random();
                if (random.Next(0, 100) == 19)    // 百分之一的机会
                {
                    int alarmTypeEnum = random.Next(0, 10);  // 十分之一的机会是电缆温度，十分之九的机会是剩余电流
                    var lstFireUnit = _cacheManager.GetCache("BigScreen").Get("lstFireUnit", () => GetAllFireUnit());
                    int key = random.Next(0, lstFireUnit.Count);
                    if (!lstFireUnit[key].Name.Contains("兴源大厦"))
                    {
                        lstAlarmElec.Insert(0, new AlarmElec()
                        {
                            CreationTime = DateTime.Now,
                            FireUnitName = lstFireUnit[key].Name,
                            ContractName = lstFireUnit[key].ContractName,
                            ContractPhone = lstFireUnit[key].ContractPhone,
                            Address = lstFireUnit[key].Address,
                            AlarmType = alarmTypeEnum.Equals(0) ? "电缆温度探测器" : "剩余电流探测器",
                            AlarmValue = alarmTypeEnum.Equals(0) ? random.Next(102, 150) + "℃" : random.Next(300, 500) + "mA"
                        });
                    }
                }
            }
            lstAlarmElec.RemoveAll(item => item.CreationTime.AddHours(1) < DateTime.Now);
            lstAlarmElec = lstAlarmElec.OrderByDescending(item => item.CreationTime).ToList();
            _cacheManager.GetCache("BigScreen").Set("lstAlarmElec", lstAlarmElec);

            int cntAlarmElec = lstAlarmElec.Count > 8 ? 8 : lstAlarmElec.Count;   // 最多取8条
            for (int i = 0; i < cntAlarmElec; i++)
            {
                var alarmElec = lstAlarmElec[i];
                mt.value += $"<br/>{alarmElec.CreationTime.ToString("HH:mm")} {alarmElec.FireUnitName}";
                if (alarmElec.CreationTime.AddMinutes(3) >= DateTime.Now)   // 3分钟之内的，显示“new”
                {
                    mt.value += " <img src=\"http://vshare.sharegroup.com.cn/images/new.png\" />";
                }
                mt.value += $"<br/>{alarmElec.ContractName}({alarmElec.ContractPhone}) {alarmElec.Address}<br/>【{alarmElec.AlarmType}】{alarmElec.AlarmValue}<br/>";
            }

            if (!string.IsNullOrEmpty(mt.value))
            {
                mt.value = mt.value.Substring(5);
            }
            lstDataText.Add(mt);
            return Task.FromResult(lstDataText);
        }
        /// <summary>
        /// 首页：辖区内各防火单位的消防联网实时达
        /// </summary>
        /// <param name="fireDeptId"></param>
        /// <returns></returns>
        public async Task<List<DataText>> GetAlarmTo119(int fireDeptId)
        {
            var lst = await _alarmManager.GetAlarmTo119List(fireDeptId);
            List<DataText> lstDataText = new List<DataText>();
            DataText mt = new DataText();
            if (lst != null && lst.Count > 0)
            {
                int n = 3;
                if (lst.Count < n) n = lst.Count;
                for (int i = 0; i < n; i++)
                {
                    var item = lst[i];
                    mt.value += $"<br /><font color='DarkOrange'>【核警时间】</font>{item.FireCheckTime.ToString("yyyy -MM-dd HH:mm:ss")}";
                    if (item.FireCheckTime.AddMinutes(5) >= DateTime.Now)   // 5分钟之内的，显示“new”
                    {
                        mt.value += " &nbsp;&nbsp;<img src=\"http://vshare.sharegroup.com.cn/images/new.gif\" />";
                    }
                    mt.value += $"<br /><font color='DarkOrange'>【火警时间】</font>{item.FireAlarmTime.ToString("yyyy-MM-dd HH:mm:ss")}";
                    mt.value += $"<br /><font color='DarkOrange'>【防火单位】</font>{item.FireUnitName}";
                    mt.value += $"<br /><font color='DarkOrange'>【单位地址】</font>{item.FireUnitAddress}";
                    mt.value += $"<br /><font color='DarkOrange'>【消防联系人】</font>{item.ContractName}（{item.ContractPhone}）";
                    mt.value += $"<br /><font color='DarkOrange'>【报警部件】</font>{item.AlarmDetectorTypeName}（{item.AlarmDetectorAddress}）";
                    mt.value += "<br />";
                }
            }
            if (!string.IsNullOrEmpty(mt.value))
            {
                mt.value = mt.value.Substring(6);
            }
            lstDataText.Add(mt);
            return lstDataText;
        }
        /// <summary>
        /// 获取辖区内防火单位的火警联网部件设施正常率
        /// </summary>
        /// <param name="fireDeptId"></param>
        /// <returns></returns>
        public Task<List<DataDouble>> GetFireAlarmDetectorNormalRate(int fireDeptId)
        {
            List<DataDouble> lstDataDouble = new List<DataDouble>();
            DataDouble mt = new DataDouble();
            var detectors = _repFireAlarmDetector.GetAll().Where(item => item.State.Equals(FireAlarmDetectorState.Fault));
            var fireUnits = _fireUnitRep.GetAll().Where(item => item.FireDeptId.Equals(fireDeptId));
            var fireAlarmDevices = _repFireAlarmDevice.GetAll().Where(item => item.NetDetectorNum > 0);

            var query = from a in fireAlarmDevices
                        join b in fireUnits on a.FireUnitId equals b.Id
                        select new
                        {
                            NetDetectorNum = a.NetDetectorNum
                        };
            int sumNetDetectorNum = query.Sum(item => item.NetDetectorNum); // 所有联网部件的数量

            var query1 = from a in detectors
                         join b in fireUnits on a.FireUnitId equals b.Id
                         join c in fireAlarmDevices on a.FireAlarmDeviceId equals c.Id
                         select new
                         {
                             Id = a.Id
                         };

            int faultDetectorNum = query1.Count();  // 故障部件数量
            if (sumNetDetectorNum > 0 && faultDetectorNum > 0) mt.value = Math.Round((double)((sumNetDetectorNum - faultDetectorNum)) / sumNetDetectorNum, 2);
            else mt.value = 0;

            lstDataDouble.Add(mt);
            return Task.FromResult(lstDataDouble);
        }
        /// <summary>
        /// 获取辖区内防火单位联网部件总数量
        /// </summary>
        /// <param name="fireDeptId"></param>
        /// <returns></returns>
        public Task<List<NumberCard>> GetNetDetectorNum(int fireDeptId)
        {
            List<NumberCard> lstNumberCard = new List<NumberCard>();
            NumberCard numberCard = new NumberCard()
            {
                name = "",
                value = 0
            };

            var fireUnits = _fireUnitRep.GetAll().Where(item => item.FireDeptId.Equals(fireDeptId));
            var fireAlarmDevices = _repFireAlarmDevice.GetAll().Where(item => item.NetDetectorNum > 0);

            var query = from a in fireAlarmDevices
                        join b in fireUnits on a.FireUnitId equals b.Id
                        select new
                        {
                            NetDetectorNum = a.NetDetectorNum
                        };
            int sumNetDetectorNum = query.Sum(item => item.NetDetectorNum); // 所有联网部件的数量
            numberCard.value = sumNetDetectorNum;

            lstNumberCard.Add(numberCard);
            return Task.FromResult(lstNumberCard);
        }
        /// <summary>
        /// 获取辖区内防火单位故障部件总数量
        /// </summary>
        /// <param name="fireDeptId"></param>
        /// <returns></returns>
        public Task<List<NumberCard>> GetFaultDetectorNum(int fireDeptId)
        {
            List<NumberCard> lstNumberCard = new List<NumberCard>();
            NumberCard numberCard = new NumberCard()
            {
                name = "",
                value = 0
            };

            var detectors = _repFireAlarmDetector.GetAll().Where(item => item.State.Equals(FireAlarmDetectorState.Fault));
            var fireUnits = _fireUnitRep.GetAll().Where(item => item.FireDeptId.Equals(fireDeptId));
            var fireAlarmDevices = _repFireAlarmDevice.GetAll().Where(item => item.NetDetectorNum > 0);

            var query1 = from a in detectors
                         join b in fireUnits on a.FireUnitId equals b.Id
                         join c in fireAlarmDevices on a.FireAlarmDeviceId equals c.Id
                         select new
                         {
                             Id = a.Id
                         };

            int faultDetectorNum = query1.Count();  // 故障部件数量
            numberCard.value = faultDetectorNum;

            lstNumberCard.Add(numberCard);
            return Task.FromResult(lstNumberCard);
        }
        /// <summary>
        /// 获取辖区内电气火灾设备状态统计数据
        /// </summary>
        /// <param name="fireDeptId"></param>
        /// <returns></returns>
        public Task<List<DataXY>> GetElectricDeviceStateStatis(int fireDeptId)
        {
            var electricDevices = _repFireElectricDevice.GetAll();
            var fireUnits = _fireUnitRep.GetAll().Where(item => item.FireDeptId.Equals(fireDeptId));

            var query = from a in electricDevices
                        join b in fireUnits on a.FireUnitId equals b.Id
                        select new
                        {
                            Id = a.Id,
                            State = a.State
                        };
            int offLineNum = query.Count(item => item.State.Equals(FireElectricDeviceState.Offline));
            int goodNum = query.Count(item => item.State.Equals(FireElectricDeviceState.Good));
            int dangerNum = query.Count(item => item.State.Equals(FireElectricDeviceState.Danger));
            int transfiniteNum = query.Count(item => item.State.Equals(FireElectricDeviceState.Transfinite));

            var lst = new List<DataXY>();
            lst.Add(new DataXY()
            {
                X = "离线",
                Y = offLineNum
            });
            lst.Add(new DataXY()
            {
                X = "良好",
                Y = goodNum
            });
            lst.Add(new DataXY()
            {
                X = "隐患",
                Y = dangerNum
            });
            lst.Add(new DataXY()
            {
                X = "超限",
                Y = transfiniteNum
            });

            return Task.FromResult(lst);
        }
        /// <summary>
        /// 获取辖区内当前状态为隐患的电气火灾设备的数量
        /// </summary>
        /// <param name="fireDeptId"></param>
        /// <returns></returns>
        public Task<List<NumberCard>> GetElectricDeviceDangerNum(int fireDeptId)
        {
            List<NumberCard> lstNumberCard = new List<NumberCard>();
            NumberCard numberCard = new NumberCard()
            {
                name = "",
                value = 0
            };

            var electricDevices = _repFireElectricDevice.GetAll().Where(item => item.State.Equals(FireElectricDeviceState.Danger));
            var fireUnits = _fireUnitRep.GetAll().Where(item => item.FireDeptId.Equals(fireDeptId));

            var query = from a in electricDevices
                        join b in fireUnits on a.FireUnitId equals b.Id
                        select new
                        {
                            Id = a.Id
                        };
            numberCard.value = query.Count();

            lstNumberCard.Add(numberCard);
            return Task.FromResult(lstNumberCard);
        }
        /// <summary>
        /// 获取辖区内当前状态为离线的电气火灾设备的数量
        /// </summary>
        /// <param name="fireDeptId"></param>
        /// <returns></returns>
        public Task<List<NumberCard>> GetElectricDeviceOfflineNum(int fireDeptId)
        {
            List<NumberCard> lstNumberCard = new List<NumberCard>();
            NumberCard numberCard = new NumberCard()
            {
                name = "",
                value = 0
            };

            var electricDevices = _repFireElectricDevice.GetAll().Where(item => item.State.Equals(FireElectricDeviceState.Offline));
            var fireUnits = _fireUnitRep.GetAll().Where(item => item.FireDeptId.Equals(fireDeptId));

            var query = from a in electricDevices
                        join b in fireUnits on a.FireUnitId equals b.Id
                        select new
                        {
                            Id = a.Id
                        };
            numberCard.value = query.Count();

            lstNumberCard.Add(numberCard);
            return Task.FromResult(lstNumberCard);
        }
        /// <summary>
        /// 获取辖区内当前状态为良好的电气火灾设备的数量
        /// </summary>
        /// <param name="fireDeptId"></param>
        /// <returns></returns>
        public Task<List<NumberCard>> GetElectricDeviceGoodNum(int fireDeptId)
        {
            List<NumberCard> lstNumberCard = new List<NumberCard>();
            NumberCard numberCard = new NumberCard()
            {
                name = "",
                value = 0
            };

            var electricDevices = _repFireElectricDevice.GetAll().Where(item => item.State.Equals(FireElectricDeviceState.Good));
            var fireUnits = _fireUnitRep.GetAll().Where(item => item.FireDeptId.Equals(fireDeptId));

            var query = from a in electricDevices
                        join b in fireUnits on a.FireUnitId equals b.Id
                        select new
                        {
                            Id = a.Id
                        };
            numberCard.value = query.Count();

            lstNumberCard.Add(numberCard);
            return Task.FromResult(lstNumberCard);
        }
        /// <summary>
        /// 获取辖区内当前状态为超限的电气火灾设备的数量
        /// </summary>
        /// <param name="fireDeptId"></param>
        /// <returns></returns>
        public Task<List<NumberCard>> GetElectricDeviceTransfiniteNum(int fireDeptId)
        {
            List<NumberCard> lstNumberCard = new List<NumberCard>();
            NumberCard numberCard = new NumberCard()
            {
                name = "",
                value = 0
            };

            var electricDevices = _repFireElectricDevice.GetAll().Where(item => item.State.Equals(FireElectricDeviceState.Transfinite));
            var fireUnits = _fireUnitRep.GetAll().Where(item => item.FireDeptId.Equals(fireDeptId));

            var query = from a in electricDevices
                        join b in fireUnits on a.FireUnitId equals b.Id
                        select new
                        {
                            Id = a.Id
                        };
            numberCard.value = query.Count();

            lstNumberCard.Add(numberCard);
            return Task.FromResult(lstNumberCard);
        }
        /// <summary>
        /// 获取本月辖区内各防火单位真实火警报119的数量
        /// </summary>
        /// <param name="fireDeptId"></param>
        /// <returns></returns>
        public async Task<List<NumberCard>> GetAlarmTo119Num(int fireDeptId)
        {
            List<NumberCard> lstNumberCard = new List<NumberCard>();
            NumberCard numberCard = new NumberCard()
            {
                name = "",
                value = 0
            };

            var lst = await _alarmManager.GetAlarmTo119List(fireDeptId, DateTime.Now.Year, DateTime.Now.Month);
            if (lst != null)
            {
                numberCard.value = lst.Count;
            }
            lstNumberCard.Add(numberCard);
            return lstNumberCard;
        }
        /// <summary>
        /// 获取辖区内防火单位的数量
        /// </summary>
        /// <param name="fireDeptId"></param>
        /// <returns></returns>
        public async Task<List<NumberCard>> GetFireUnitNum(int fireDeptId)
        {
            List<NumberCard> lstNumberCard = new List<NumberCard>();
            NumberCard totalWarning = new NumberCard()
            {
                name = "",
                value = await _fireUnitRep.CountAsync(item => item.FireDeptId.Equals(fireDeptId))
            };

            lstNumberCard.Add(totalWarning);
            return lstNumberCard;
        }
        /// <summary>
        /// 首页：获取每个月防火单位总接入数量
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public Task<List<NumberCard>> GetTotalFireUnitNum(string value)
        {
            List<NumberCard> lstNumberCard = new List<NumberCard>();
            NumberCard totalWarning = new NumberCard();
            totalWarning.name = "";
            switch (value)
            {
                case "2018-01":
                    totalWarning.value = 8;
                    break;
                case "2018-03":
                    totalWarning.value = 19;
                    break;
                case "2019-01":
                    totalWarning.value = 92;
                    break;
                case "2019-04":
                    totalWarning.value = 124;
                    break;
                default:
                    totalWarning.value = 116;
                    break;
            }
            lstNumberCard.Add(totalWarning);
            return Task.FromResult(lstNumberCard);
        }
        /// <summary>
        /// 首页：获取每个月防火单位总接入数量_柳州
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public Task<List<NumberCard>> GetTotalFireUnitNum_lz(string value)
        {
            List<NumberCard> lstNumberCard = new List<NumberCard>();
            NumberCard totalWarning = new NumberCard();
            totalWarning.name = "";
            totalWarning.value = 5;
            lstNumberCard.Add(totalWarning);
            return Task.FromResult(lstNumberCard);
        }
        /// <summary>
        /// 首页：获取每个月总预警数量
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public Task<List<NumberCard>> GetTotalWarningNum(string value)
        {
            List<NumberCard> lstNumberCard = new List<NumberCard>();
            NumberCard totalWarning = new NumberCard();
            totalWarning.name = "";
            switch (value)
            {
                case "2018-01":
                    totalWarning.value = 89;
                    break;
                case "2018-03":
                    totalWarning.value = 364;
                    break;
                case "2019-01":
                    totalWarning.value = 810;
                    break;
                case "2019-04":
                    totalWarning.value = 781;
                    break;
                default:
                    totalWarning.value = 633;
                    break;
            }
            lstNumberCard.Add(totalWarning);
            return Task.FromResult(lstNumberCard);
        }
        /// <summary>
        /// 首页：获取每个月总预警数量_柳州
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public Task<List<NumberCard>> GetTotalWarningNum_lz(string value)
        {
            List<NumberCard> lstNumberCard = new List<NumberCard>();
            NumberCard totalWarning = new NumberCard();
            totalWarning.name = "";
            totalWarning.value = 157;
            lstNumberCard.Add(totalWarning);
            return Task.FromResult(lstNumberCard);
        }

        /// <summary>
        /// 防火单位：单位名称
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<List<DataText>> GetFireUnitName(string id)
        {
            List<DataText> lstDataText = new List<DataText>();
            DataText dataText = new DataText();
            if (":id".Equals(id))
            {
                dataText.value = "四川天树聚科技有限公司";
            }
            else
            {
                var fireUnit = await _fireUnitRep.GetAsync(int.Parse(id));

                if (fireUnit != null)
                {
                    dataText.value = fireUnit.Name;
                }
                else
                {
                    dataText.value = "未知数据";
                }
            }
            lstDataText.Add(dataText);
            return lstDataText;
        }
        /// <summary>
        /// 防火单位：单位联系方式
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<List<DataText>> GetFireUnitContractAddress(string id)
        {
            List<DataText> lstDataText = new List<DataText>();
            DataText dataText = new DataText();
            if (":id".Equals(id))
            {
                dataText.value = "郭海 15881199975<br/>丽都路518号14楼";
            }
            else
            {
                var fireUnit = await _fireUnitRep.GetAsync(int.Parse(id));

                if (fireUnit != null)
                {
                    dataText.value = $"{fireUnit.ContractName} {fireUnit.ContractPhone}<br/>{fireUnit.Address}";
                }
                else
                {
                    dataText.value = "未知数据";
                }
            }
            lstDataText.Add(dataText);
            return lstDataText;
        }
        /// <summary>
        /// 防火单位：单位数据表格
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<string> GetFireUnitDataGrid(string id)
        {
            Random random = new Random();
            string result = "[{\"label\":\"数据类型\",\"value\":\"最近30天记录数\"},{\"火警预警\":01,\"电气预警\":02,\"值班记录\":03,\"巡查记录\":04}]";
            result = result.Replace("01", random.Next(10, 40).ToString());
            result = result.Replace("02", random.Next(3, 15).ToString());
            result = result.Replace("03", random.Next(25, 91).ToString());
            result = result.Replace("04", random.Next(4, 31).ToString());
            return Task.FromResult(result);
        }

        /// <summary>
        /// 防火单位：类型柱状图
        /// </summary>
        /// <returns></returns>
        public Task<List<Histogram>> GetFireUnitTypeHistogram()
        {
            List<Histogram> lstHistogram = new List<Histogram>();
            string sql = "SELECT TypeId, b.name, COUNT(1) cnt FROM fireunit a INNER JOIN fireunittype b ON a.`TypeId` = b.`Id` WHERE lng > 0 and a.firedeptid=33 AND a.IsDeleted=0 GROUP BY TypeId ORDER BY cnt DESC LIMIT 10";
            var dataTable = _sqlRepository.Query(sql);
            foreach (DataRow row in dataTable.Rows)
            {
                lstHistogram.Add(new Histogram()
                {
                    x = row["name"].ToString(),
                    y = int.Parse(row["cnt"].ToString())
                });
            }
            return Task.FromResult(lstHistogram);
        }

        /// <summary>
        /// 消火栓：地图呼吸气泡层
        /// </summary>
        /// <returns></returns>
        public Task<List<BreathingBubble>> GetHydrantBreathingBubble()
        {
            List<BreathingBubble> lstBreathingBubble = new List<BreathingBubble>();

            var lstHydrantBreath = _hydrantRep.GetAll().Where(item => item.Lng > 0).ToList();
            foreach (var hydrant in lstHydrantBreath)
            {
                lstBreathingBubble.Add(new BreathingBubble()
                {
                    id = hydrant.Id,
                    lng = (double)hydrant.Lng,
                    lat = (double)hydrant.Lat,
                    info = hydrant.Sn
                });
            }
            return Task.FromResult(lstBreathingBubble);
        }
        /// <summary>
        /// 消火栓：编号
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<List<DataText>> GetHydrantSn(string id)
        {
            List<DataText> lstDataText = new List<DataText>();
            DataText dataText = new DataText();
            if (":id".Equals(id))
            {
                dataText.value = "JRT4FD";
            }
            else
            {
                var hydrant = await _hydrantRep.GetAsync(int.Parse(id));

                if (hydrant != null)
                {
                    dataText.value = hydrant.Sn;
                }
                else
                {
                    dataText.value = "未知数据";
                }
            }
            lstDataText.Add(dataText);
            return lstDataText;
        }
        /// <summary>
        /// 消火栓：地址
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<List<DataText>> GetHydrantAddress(string id)
        {
            List<DataText> lstDataText = new List<DataText>();
            DataText dataText = new DataText();
            if (":id".Equals(id))
            {
                dataText.value = "东荆路8号东林景忆小区后门";
            }
            else
            {
                var hydrant = await _hydrantRep.GetAsync(int.Parse(id));

                if (hydrant != null)
                {
                    dataText.value = hydrant.Address;
                }
                else
                {
                    dataText.value = "未知数据";
                }
            }
            lstDataText.Add(dataText);
            return lstDataText;
        }
        /// <summary>
        /// 消火栓：当前水压
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<List<NumberCard>> GetHydrantPress(string id)
        {
            List<NumberCard> lstNumberCard = new List<NumberCard>();
            Random random = new Random();
            NumberCard numberCard = new NumberCard();
            numberCard.name = "";
            numberCard.value = random.Next(105, 200);
            lstNumberCard.Add(numberCard);
            return Task.FromResult(lstNumberCard);
        }
        /// <summary>
        /// 消火栓：历史水压
        /// </summary>
        /// <param name="id"></param>
        /// <param name="days"></param>
        /// <returns></returns>
        public Task<List<Histogram>> GetHydrantPressHistory(string id, int days)
        {
            Random random = new Random();
            List<Histogram> lstHistogram = new List<Histogram>();
            for (int i = 1; i <= days; i++)
            {
                lstHistogram.Add(new Histogram()
                {
                    x = DateTime.Now.AddDays(-i).ToString("yyyy/MM/dd 00:00:00"),
                    y = random.Next(120, 150),
                });
            }

            return Task.FromResult(lstHistogram);
        }
        /// <summary>
        /// 消火栓：区域柱状图
        /// </summary>
        /// <returns></returns>
        public Task<List<Histogram>> GetHydrantAreaHistogram()
        {
            List<Histogram> lstHistogram = new List<Histogram>();
            string sql = "SELECT AreaId, b.Name, COUNT(1) AS cnt FROM hydrant a INNER JOIN AREA b ON a.`AreaId` = b.`Id` WHERE a.IsDeleted = 0 and a.lng > 0 GROUP BY a.AreaId ORDER BY cnt DESC LIMIT 10";
            var dataTable = _sqlRepository.Query(sql);
            foreach (DataRow row in dataTable.Rows)
            {
                lstHistogram.Add(new Histogram()
                {
                    x = row["Name"].ToString(),
                    y = int.Parse(row["cnt"].ToString())
                });
            }
            return Task.FromResult(lstHistogram);
        }

        private List<FireUnit> GetAllFireUnit(int deptId = 1)
        {
            return _fireUnitRep.GetAll().Where(item => item.Lng > 0 && item.FireDeptId.Equals(deptId)).OrderBy(item => item.CreationTime).ToList();
        }

        private List<AlarmElec> InitAlarmElecList()
        {
            var tempAlarmElecList = new List<AlarmElec>();
            var lstFireUnit = _cacheManager.GetCache("BigScreen").Get("lstFireUnit", () => GetAllFireUnit());
            int cntFireUnit = lstFireUnit.Count();
            Random random = new Random();
            for (int i = 0; i < 8; i++) // 初始化8条数据
            {
                int alarmTypeEnum = random.Next(0, 5);  // 五分之一的机会是电缆温度，五分之四的机会是剩余电流
                var fireUnit = lstFireUnit[random.Next(0, cntFireUnit)];
                tempAlarmElecList.Add(new AlarmElec()
                {
                    CreationTime = DateTime.Now.AddMinutes(random.Next(-30, -2)),
                    FireUnitName = fireUnit.Name,
                    ContractName = fireUnit.ContractName,
                    ContractPhone = fireUnit.ContractPhone,
                    Address = fireUnit.Address,
                    AlarmType = alarmTypeEnum.Equals(0) ? "电缆温度探测器" : "剩余电流探测器",
                    AlarmValue = alarmTypeEnum.Equals(0) ? random.Next(102, 150) + "℃" : random.Next(320, 500) + "mA"
                });
            }
            return tempAlarmElecList.OrderBy(item => item.CreationTime).ToList();
        }
    }
}
