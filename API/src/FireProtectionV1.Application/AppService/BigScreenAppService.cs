﻿using Abp.Application.Services;
using Abp.Web.Models;
using FireProtectionV1.BigScreen.Dto;
using FireProtectionV1.BigScreen.Manager;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FireProtectionV1.AppService
{
    [DontWrapResult]
    public class BigScreenAppService : AppServiceBase
    {
        IBigScreenManager _manager;

        public BigScreenAppService(IBigScreenManager manager)
        {
            _manager = manager;
        }

        /// <summary>
        /// 首页：地图呼吸气泡层
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public async Task<List<BreathingBubble>> GetBreathingBubble(string value)
        {
            return await _manager.GetBreathingBubble(value);
        }

        /// <summary>
        /// 首页：地图呼吸气泡层_柳州
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public async Task<List<BreathingBubble>> GetBreathingBubble_lz(string value)
        {
            return await _manager.GetBreathingBubble_lz(value);
        }

        /// <summary>
        /// 首页：飞线层
        /// </summary>
        /// <returns></returns>
        public async Task<List<FlyLine>> GetFlyLine()
        {
            return await _manager.GetFlyLine();
        }
        /// <summary>
        /// 首页：飞线层_柳州
        /// </summary>
        /// <returns></returns>
        public async Task<List<FlyLine_lz>> GetFlyLine_lz()
        {
            return await _manager.GetFlyLine_lz();
        }

        /// <summary>
        /// 首页：地图多行文本
        /// </summary>
        /// <param name="fireDeptId"></param>
        /// <returns></returns>
        public async Task<List<DataText>> GetMapMultiText(int fireDeptId)
        {
            return await _manager.GetMapMultiText(fireDeptId);
        }
        /// <summary>
        /// 首页：电气警情天讯通
        /// </summary>
        /// <returns></returns>
        public async Task<List<DataText>> GetTianXunTong()
        {
            return await _manager.GetTianXunTong();
        }
        /// <summary>
        /// 首页：辖区内各防火单位的消防联网实时达
        /// </summary>
        /// <param name="fireDeptId"></param>
        /// <returns></returns>
        public async Task<List<DataText>> GetAlarmTo119(int fireDeptId)
        {
            return await _manager.GetAlarmTo119(fireDeptId);
        }
        /// <summary>
        /// 获取本月辖区内各防火单位真实火警报119的数量
        /// </summary>
        /// <param name="fireDeptId"></param>
        /// <returns></returns>
        public async Task<List<NumberCard>> GetAlarmTo119Num(int fireDeptId)
        {
            return await _manager.GetAlarmTo119Num(fireDeptId);
        }
        /// <summary>
        /// 获取辖区内防火单位的数量
        /// </summary>
        /// <param name="fireDeptId"></param>
        /// <returns></returns>
        public async Task<List<NumberCard>> GetFireUnitNum(int fireDeptId)
        {
            return await _manager.GetFireUnitNum(fireDeptId);
        }
        /// <summary>
        /// 获取辖区内防火单位的火警联网部件设施正常率
        /// </summary>
        /// <param name="fireDeptId"></param>
        /// <returns></returns>
        public async Task<List<DataDouble>> GetFireAlarmDetectorNormalRate(int fireDeptId)
        {
            return await _manager.GetFireAlarmDetectorNormalRate(fireDeptId);
        }
        /// <summary>
        /// 获取辖区内防火单位联网部件总数量
        /// </summary>
        /// <param name="fireDeptId"></param>
        /// <returns></returns>
        public async Task<List<NumberCard>> GetNetDetectorNum(int fireDeptId)
        {
            return await _manager.GetNetDetectorNum(fireDeptId);
        }
        /// <summary>
        /// 获取辖区内防火单位故障部件总数量
        /// </summary>
        /// <param name="fireDeptId"></param>
        /// <returns></returns>
        public async Task<List<NumberCard>> GetFaultDetectorNum(int fireDeptId)
        {
            return await _manager.GetFaultDetectorNum(fireDeptId);
        }
        /// <summary>
        /// 获取辖区内电气火灾设备状态统计数据
        /// </summary>
        /// <param name="fireDeptId"></param>
        /// <returns></returns>
        public async Task<List<DataXY>> GetElectricDeviceStateStatis(int fireDeptId)
        {
            return await _manager.GetElectricDeviceStateStatis(fireDeptId);
        }
        /// <summary>
        /// 获取辖区内当前状态为离线的电气火灾设备的数量
        /// </summary>
        /// <param name="fireDeptId"></param>
        /// <returns></returns>
        public async Task<List<NumberCard>> GetElectricDeviceOfflineNum(int fireDeptId)
        {
            return await _manager.GetElectricDeviceOfflineNum(fireDeptId);
        }
        /// <summary>
        /// 获取辖区内当前状态为良好的电气火灾设备的数量
        /// </summary>
        /// <param name="fireDeptId"></param>
        /// <returns></returns>
        public async Task<List<NumberCard>> GetElectricDeviceGoodNum(int fireDeptId)
        {
            return await _manager.GetElectricDeviceGoodNum(fireDeptId);
        }
        /// <summary>
        /// 获取辖区内当前状态为隐患的电气火灾设备的数量
        /// </summary>
        /// <param name="fireDeptId"></param>
        /// <returns></returns>
        public async Task<List<NumberCard>> GetElectricDeviceDangerNum(int fireDeptId)
        {
            return await _manager.GetElectricDeviceDangerNum(fireDeptId);
        }
        /// <summary>
        /// 获取辖区内当前状态为超限的电气火灾设备的数量
        /// </summary>
        /// <param name="fireDeptId"></param>
        /// <returns></returns>
        public async Task<List<NumberCard>> GetElectricDeviceTransfiniteNum(int fireDeptId)
        {
            return await _manager.GetElectricDeviceTransfiniteNum(fireDeptId);
        }
        /// <summary>
        /// 首页：获取每个月防火单位总接入数量
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public async Task<List<NumberCard>> GetTotalFireUnitNum(string value)
        {
            return await _manager.GetTotalFireUnitNum(value);
        }
        /// <summary>
        /// 首页：获取每个月防火单位总接入数量_柳州
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public async Task<List<NumberCard>> GetTotalFireUnitNum_lz(string value)
        {
            return await _manager.GetTotalFireUnitNum_lz(value);
        }
        /// <summary>
        /// 首页：获取每个月总预警数量
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public async Task<List<NumberCard>> GetTotalWarningNum(string value)
        {
            return await _manager.GetTotalWarningNum(value);
        }
        /// <summary>
        /// 首页：获取每个月总预警数量_柳州
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public async Task<List<NumberCard>> GetTotalWarningNum_lz(string value)
        {
            return await _manager.GetTotalWarningNum_lz(value);
        }

        /// <summary>
        /// 防火单位：单位名称
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<List<DataText>> GetFireUnitName(string id)
        {
            return await _manager.GetFireUnitName(id);
        }
        /// <summary>
        /// 防火单位：单位联系方式
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<List<DataText>> GetFireUnitContractAddress(string id)
        {
            return await _manager.GetFireUnitContractAddress(id);
        }
        /// <summary>
        /// 防火单位：单位数据表格
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<string> GetFireUnitDataGrid(string id)
        {
            return await _manager.GetFireUnitDataGrid(id);
        }
        /// <summary>
        /// 防火单位：类型柱状图
        /// </summary>
        /// <returns></returns>
        public async Task<List<Histogram>> GetFireUnitTypeHistogram()
        {
            return await _manager.GetFireUnitTypeHistogram();
        }
        /// <summary>
        /// 消火栓：地图呼吸气泡层
        /// </summary>
        /// <returns></returns>
        public async Task<List<BreathingBubble>> GetHydrantBreathingBubble()
        {
            return await _manager.GetHydrantBreathingBubble();
        }
        /// <summary>
        /// 消火栓：编号
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<List<DataText>> GetHydrantSn(string id)
        {
            return await _manager.GetHydrantSn(id);
        }
        /// <summary>
        /// 消火栓：地址
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<List<DataText>> GetHydrantAddress(string id)
        {
            return await _manager.GetHydrantAddress(id);
        }
        /// <summary>
        /// 消火栓：当前水压
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<List<NumberCard>> GetHydrantPress(string id)
        {
            return await _manager.GetHydrantPress(id);
        }
        /// <summary>
        /// 消火栓：历史水压
        /// </summary>
        /// <param name="id"></param>
        /// <param name="days"></param>
        /// <returns></returns>
        public async Task<List<Histogram>> GetHydrantPressHistory(string id, int days = 7)
        {
            return await _manager.GetHydrantPressHistory(id, days);
        }
        /// <summary>
        /// 消火栓：区域柱状图
        /// </summary>
        /// <returns></returns>
        public async Task<List<Histogram>> GetHydrantAreaHistogram()
        {
            return await _manager.GetHydrantAreaHistogram();
        }
        
    }
}
