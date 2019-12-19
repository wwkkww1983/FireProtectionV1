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
        Task<List<NumberCard>> GetTotalWarningNum(string value);
        /// <summary>
        /// 首页：获取每个月总预警数量_柳州
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        Task<List<NumberCard>> GetTotalWarningNum_lz(string value);
        /// <summary>
        /// 首页：获取每个月防火单位总接入数量
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        Task<List<NumberCard>> GetTotalFireUnitNum(string value);
        /// <summary>
        /// 首页：获取每个月防火单位总接入数量_柳州
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        Task<List<NumberCard>> GetTotalFireUnitNum_lz(string value);
        /// <summary>
        /// 首页：地图呼吸气泡层
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        Task<List<BreathingBubble>> GetBreathingBubble(string value);
        /// <summary>
        /// 首页：地图呼吸气泡层_柳州
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        Task<List<BreathingBubble>> GetBreathingBubble_lz(string value);
        /// <summary>
        /// 首页：飞线层
        /// </summary>
        /// <returns></returns>
        Task<List<FlyLine>> GetFlyLine();
        /// <summary>
        /// 首页：飞线层_柳州
        /// </summary>
        /// <returns></returns>
        Task<List<FlyLine_lz>> GetFlyLine_lz();
        /// <summary>
        /// 首页：地图多行文本
        /// </summary>
        /// <returns></returns>
        Task<List<DataText>> GetMapMultiText();
        /// <summary>
        /// 首页：电气警情天讯通
        /// </summary>
        /// <returns></returns>
        Task<List<DataText>> GetTianXunTong();
        /// <summary>
        /// 防火单位：单位名称
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<List<DataText>> GetFireUnitName(string id);
        /// <summary>
        /// 防火单位：单位联系方式
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<List<DataText>> GetFireUnitContractAddress(string id);
        /// <summary>
        /// 防火单位：单位数据表格
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<string> GetFireUnitDataGrid(string id);
        /// <summary>
        /// 防火单位：类型柱状图
        /// </summary>
        /// <returns></returns>
        Task<List<Histogram>> GetFireUnitTypeHistogram();
        /// <summary>
        /// 消火栓：地图呼吸气泡层
        /// </summary>
        /// <returns></returns>
        Task<List<BreathingBubble>> GetHydrantBreathingBubble();
        /// <summary>
        /// 消火栓：编号
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<List<DataText>> GetHydrantSn(string id);
        /// <summary>
        /// 消火栓：地址
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<List<DataText>> GetHydrantAddress(string id);
        /// <summary>
        /// 消火栓：当前水压
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<List<NumberCard>> GetHydrantPress(string id);
        /// <summary>
        /// 消火栓：历史水压
        /// </summary>
        /// <param name="id"></param>
        /// <param name="days"></param>
        /// <returns></returns>
        Task<List<Histogram>> GetHydrantPressHistory(string id, int days);
        /// <summary>
        /// 消火栓：区域柱状图
        /// </summary>
        /// <returns></returns>
        Task<List<Histogram>> GetHydrantAreaHistogram();
    }
}
