using Abp.Domain.Repositories;
using Abp.Domain.Services;
using Abp.Runtime.Caching;
using DeviceServer.Tcp.Protocol;
using FireProtectionV1.BigScreen.Dto;
using FireProtectionV1.Common.DBContext;
using FireProtectionV1.Enterprise.Model;
using FireProtectionV1.FireWorking.Manager;
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
        IRepository<FireUnitType> _fireUnitTypeRep;
        IRepository<Hydrant> _hydrantRep;
        ICacheManager _cacheManager;
        ISqlRepository _sqlRepository;
        public BigScreenManager(
            IDeviceManager deviceManager,
            IAlarmManager alarmManager,
            IRepository<FireUnit> fireUnitRep,
            IRepository<FireUnitType> fireUnitTypeRep,
            IRepository<Hydrant> hydrantRep,
            ICacheManager cacheManager,
            ISqlRepository sqlRepository)
        {
            _deviceManager = deviceManager;
            _alarmManager = alarmManager;
            _fireUnitRep = fireUnitRep;
            _fireUnitTypeRep = fireUnitTypeRep;
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
                jsq++;
                if ((jsq == 8 && "2018-01".Equals(value)) || (jsq == 19 && "2018-03".Equals(value)) || (jsq == 92 && "2019-01".Equals(value))) break;
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
        /// 首页：地图多行文本
        /// </summary>
        /// <returns></returns>
        public Task<List<DataText>> GetMapMultiText()
        {
            List<DataText> lstDataText = new List<DataText>();
            DataText mt = new DataText();
            var lstFireUnit = _cacheManager.GetCache("BigScreen").Get("lstFireUnit", () => GetAllFireUnit());
            int cntFireUnit = lstFireUnit.Count();
            Random random = new Random();
            for (int i = 0; i < 10; i++)
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
            var query = from a in _alarmManager.GetNewElecAlarm(startTime)
                        join b in _deviceManager.GetDetectorElectricAll()
                        on a.DetectorId equals b.Id
                        join c in _deviceManager.GetDetectorTypeAll()
                        on b.DetectorTypeId equals c.Id
                        join d in _fireUnitRep.GetAll()
                        on a.FireUnitId equals d.Id
                        orderby a.CreationTime descending
                        select new AlarmElec()
                        {
                            Address = d.Address,
                            AlarmType = c.Name,
                            AlarmValue = $"{a.Analog}{a.Unit}",
                            ContractName = d.ContractName,
                            ContractPhone = d.ContractPhone,
                            CreationTime = a.CreationTime,
                            FireUnitName = d.Name
                        };
            return query.ToList();
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
                    mt.value += " <img src=\"http://firea.go028.cn:8006/new.png\" />";
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
            string sql = "SELECT TypeId, b.name, COUNT(1) cnt FROM fireunit a INNER JOIN fireunittype b ON a.`TypeId` = b.`Id` WHERE lng > 0 GROUP BY TypeId ORDER BY cnt DESC LIMIT 10";
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
            string sql = "SELECT AreaId, b.Name, COUNT(1) AS cnt FROM hydrant a INNER JOIN AREA b ON a.`AreaId` = b.`Id` WHERE lng > 0 GROUP BY AreaId ORDER BY cnt DESC LIMIT 10";
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

        private List<FireUnit> GetAllFireUnit()
        {
            return _fireUnitRep.GetAll().Where(item => item.Lng > 0).OrderBy(item => item.CreationTime).ToList();
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
