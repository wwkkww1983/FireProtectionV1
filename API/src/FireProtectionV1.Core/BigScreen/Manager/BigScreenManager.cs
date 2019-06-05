﻿using Abp.Domain.Repositories;
using Abp.Domain.Services;
using Abp.Runtime.Caching;
using FireProtectionV1.BigScreen.Dto;
using FireProtectionV1.Common.DBContext;
using FireProtectionV1.Enterprise.Model;
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

        IRepository<FireUnit> _fireUnitRep;
        IRepository<FireUnitType> _fireUnitTypeRep;
        ICacheManager _cacheManager;
        ISqlRepository _sqlRepository;
        public BigScreenManager(IRepository<FireUnit> fireUnitRep, IRepository<FireUnitType> fireUnitTypeRep, ICacheManager cacheManager, ISqlRepository sqlRepository)
        {
            _fireUnitRep = fireUnitRep;
            _fireUnitTypeRep = fireUnitTypeRep;
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
                    lat = (double)fireUnit.Lat
                });
                jsq++;
                if ((jsq == 8 && "1".Equals(value)) || (jsq == 19 && "2".Equals(value)) || (jsq == 92 && "3".Equals(value))) break;
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
            foreach (var fireUnit in lstFireUnit)
            {
                lstFlyLine.Add(new FlyLine()
                {
                    from = fireUnit.Lng + "," + fireUnit.Lat
                });
            }

            return Task.FromResult(lstFlyLine);
        }
        /// <summary>
        /// 首页：地图多行文本
        /// </summary>
        /// <returns></returns>
        public Task<DataText> GetMapMultiText()
        {
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
            return Task.FromResult(mt);
        }
        /// <summary>
        /// 首页：电气警情天讯通
        /// </summary>
        /// <returns></returns>
        public Task<DataText> GetTianXunTong()
        {
            DataText mt = new DataText();
            var lstAlarmElec = _cacheManager.GetCache("BigScreen").Get("lstAlarmElec", () => new List<AlarmElec>());

            List<AlarmElec> lstAlarmElecNew = null;    // 获取CreationTime大于_AlarmElecTime的电气火灾报警数据
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
                if (random.Next(0, 10) == 9)    // 十分之一的机会
                {
                    int alarmTypeEnum = random.Next(0, 5);  // 五分之一的机会是电缆温度，五分之四的机会是剩余电流
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
                        AlarmValue = alarmTypeEnum.Equals(0) ? random.Next(102, 150) + "℃" : random.Next(320, 500) + "mA"
                    });
                }
            }

            int cntAlarmElec = lstAlarmElec.Count > 10 ? 10 : lstAlarmElec.Count;   // 最多取10条
            for (int i = 0; i < cntAlarmElec; i++)
            {
                var alarmElec = lstAlarmElec[i];
                mt.value += $"<br/>{alarmElec.CreationTime.ToString("HH:mm")} {alarmElec.FireUnitName}";
                if (alarmElec.CreationTime.AddMinutes(5) >= DateTime.Now)   // 5分钟之内的，显示“new”
                {
                    mt.value += "<img src=\"http://firea.go028.cn:8006/new.png\" />";
                }
                mt.value += $"<br/>{alarmElec.ContractName}({alarmElec.ContractPhone}) {alarmElec.Address}<br/>【{alarmElec.AlarmType}】{alarmElec.AlarmValue}<br/>";
            }

            if (!string.IsNullOrEmpty(mt.value))
            {
                mt.value = mt.value.Substring(5);
            }
            return Task.FromResult(mt);
        }
        /// <summary>
        /// 首页：获取每个月防火单位总接入数量
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public Task<NumberCard> GetTotalFireUnitNum(string value)
        {
            NumberCard totalWarning = new NumberCard();
            totalWarning.name = "";
            switch (value)
            {
                case "1":
                    totalWarning.value = 8;
                    break;
                case "2":
                    totalWarning.value = 19;
                    break;
                case "3":
                    totalWarning.value = 92;
                    break;
                case "4":
                    totalWarning.value = 116;
                    break;
                default:
                    totalWarning.value = 116;
                    break;
            }
            return Task.FromResult(totalWarning);
        }
        /// <summary>
        /// 首页：获取每个月总预警数量
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public Task<NumberCard> GetTotalWarningNum(string value)
        {
            NumberCard totalWarning = new NumberCard();
            totalWarning.name = "";
            switch (value)
            {
                case "1":
                    totalWarning.value = 689;
                    break;
                case "2":
                    totalWarning.value = 1964;
                    break;
                case "3":
                    totalWarning.value = 3310;
                    break;
                case "4":
                    totalWarning.value = 2187;
                    break;
                default:
                    totalWarning.value = 2633;
                    break;
            }
            return Task.FromResult(totalWarning);
        }

        /// <summary>
        /// 防火单位：单位名称
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<DataText> GetFireUnitName(int id)
        {
            var fireUnit = await _fireUnitRep.GetAsync(id);
            DataText dataText = new DataText();
            if (fireUnit != null)
            {
                dataText.value = fireUnit.Name;
            }
            else
            {
                dataText.value = "未知数据";
            }
            return dataText;
        }
        /// <summary>
        /// 防火单位：单位联系方式
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<DataText> GetFireUnitContractAddress(int id)
        {
            var fireUnit = await _fireUnitRep.GetAsync(id);
            DataText dataText = new DataText();
            if (fireUnit != null)
            {
                dataText.value = $"{fireUnit.ContractName} {fireUnit.ContractPhone}<br/>{fireUnit.Address}";
            }
            else
            {
                dataText.value = "未知数据";
            }
            return dataText;
        }
        /// <summary>
        /// 防火单位：单位数据表格
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<string> GetFireUnitDataGrid(int id)
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
            string sql = "SELECT TypeId, b.name, COUNT(1) cnt FROM fireunit a INNER JOIN fireunittype b ON a.`TypeId` = b.`Id` GROUP BY TypeId ORDER BY cnt DESC LIMIT 10";
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

        private List<FireUnit> GetAllFireUnit()
        {
            return _fireUnitRep.GetAll().Where(item => item.Lng > 0).OrderBy(item => item.CreationTime).ToList();
        }
    }
}
