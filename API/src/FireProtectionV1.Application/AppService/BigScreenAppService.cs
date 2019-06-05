using Abp.Application.Services;
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
        /// 首页：飞线层
        /// </summary>
        /// <returns></returns>
        public async Task<List<FlyLine>> GetFlyLine()
        {
            return await _manager.GetFlyLine();
        }
        /// <summary>
        /// 首页：地图多行文本
        /// </summary>
        /// <returns></returns>
        public async Task<DataText> GetMapMultiText()
        {
            return await _manager.GetMapMultiText();
        }
        /// <summary>
        /// 首页：电气警情天讯通
        /// </summary>
        /// <returns></returns>
        public async Task<DataText> GetTianXunTong()
        {
            return await _manager.GetTianXunTong();
        }
        /// <summary>
        /// 首页：获取每个月防火单位总接入数量
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public async Task<NumberCard> GetTotalFireUnitNum(string value)
        {
            return await _manager.GetTotalFireUnitNum(value);
        }
        /// <summary>
        /// 首页：获取每个月总预警数量
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public async Task<NumberCard> GetTotalWarningNum(string value)
        {
            return await _manager.GetTotalWarningNum(value);
        }

        /// <summary>
        /// 防火单位：单位名称
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<DataText> GetFireUnitName(int id)
        {
            return await _manager.GetFireUnitName(id);
        }
        /// <summary>
        /// 防火单位：单位联系方式
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<DataText> GetFireUnitContractAddress(int id)
        {
            return await _manager.GetFireUnitContractAddress(id);
        }
        /// <summary>
        /// 防火单位：单位数据表格
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<string> GetFireUnitDataGrid(int id)
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
    }
}
