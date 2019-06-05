using Abp.Domain.Services;
using FireProtectionV1.BigScreen.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FireProtectionV1.BigScreen.Manager
{
    public interface IBigScreenManager : IDomainService
    {
        /// <summary>
        /// 首页：获取每个月总预警数量
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        Task<NumberCard> GetTotalWarningNum(string value);
        /// <summary>
        /// 首页：获取每个月防火单位总接入数量
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        Task<NumberCard> GetTotalFireUnitNum(string value);
        /// <summary>
        /// 首页：地图呼吸气泡层
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        Task<List<BreathingBubble>> GetBreathingBubble(string value);
        /// <summary>
        /// 首页：飞线层
        /// </summary>
        /// <returns></returns>
        Task<List<FlyLine>> GetFlyLine();
        /// <summary>
        /// 首页：地图多行文本
        /// </summary>
        /// <returns></returns>
        Task<DataText> GetMapMultiText();
        /// <summary>
        /// 首页：电气警情天讯通
        /// </summary>
        /// <returns></returns>
        Task<DataText> GetTianXunTong();
        /// <summary>
        /// 防火单位：单位名称
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<DataText> GetFireUnitName(int id);
        /// <summary>
        /// 防火单位：单位联系方式
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<DataText> GetFireUnitContractAddress(int id);
        /// <summary>
        /// 防火单位：单位数据表格
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<string> GetFireUnitDataGrid(int id);
        /// <summary>
        /// 防火单位：类型柱状图
        /// </summary>
        /// <returns></returns>
        Task<List<Histogram>> GetFireUnitTypeHistogram();
    }
}
