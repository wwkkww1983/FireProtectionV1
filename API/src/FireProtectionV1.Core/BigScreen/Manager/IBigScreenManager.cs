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
        /// <param name="deptId"></param>
        /// <returns></returns>
        Task<List<DataText>> GetMapMultiText(int deptId);
        /// <summary>
        /// 获取辖区内防火单位的火警联网部件设施正常率
        /// </summary>
        /// <param name="fireDeptId"></param>
        /// <returns></returns>
        Task<List<DataDouble>> GetFireAlarmDetectorNormalRate(int fireDeptId);
        /// <summary>
        /// 首页：辖区内各防火单位的消防联网实时达
        /// </summary>
        /// <param name="fireDeptId"></param>
        /// <returns></returns>
        Task<List<DataText>> GetAlarmTo119(int fireDeptId);
        /// <summary>
        /// 获取本月辖区内各防火单位真实火警报119的数量
        /// </summary>
        /// <param name="fireDeptId"></param>
        /// <returns></returns>
        Task<List<NumberCard>> GetAlarmTo119Num(int fireDeptId);
        /// <summary>
        /// 获取辖区内防火单位联网部件总数量
        /// </summary>
        /// <param name="fireDeptId"></param>
        /// <returns></returns>
        Task<List<NumberCard>> GetNetDetectorNum(int fireDeptId);
        /// <summary>
        /// 获取辖区内电气火灾设备状态统计数据
        /// </summary>
        /// <param name="fireDeptId"></param>
        /// <returns></returns>
        Task<List<DataXY>> GetElectricDeviceStateStatis(int fireDeptId);
        /// <summary>
        /// 获取辖区内当前状态为隐患的电气火灾设备的数量
        /// </summary>
        /// <param name="fireDeptId"></param>
        /// <returns></returns>
        Task<List<NumberCard>> GetElectricDeviceDangerNum(int fireDeptId);
        /// <summary>
        /// 获取辖区内当前状态为良好的电气火灾设备的数量
        /// </summary>
        /// <param name="fireDeptId"></param>
        /// <returns></returns>
        Task<List<NumberCard>> GetElectricDeviceGoodNum(int fireDeptId);
        /// <summary>
        /// 获取辖区内当前状态为离线的电气火灾设备的数量
        /// </summary>
        /// <param name="fireDeptId"></param>
        /// <returns></returns>
        Task<List<NumberCard>> GetElectricDeviceOfflineNum(int fireDeptId);
        /// <summary>
        /// 获取辖区内当前状态为超限的电气火灾设备的数量
        /// </summary>
        /// <param name="fireDeptId"></param>
        /// <returns></returns>
        Task<List<NumberCard>> GetElectricDeviceTransfiniteNum(int fireDeptId);
        /// <summary>
        /// 获取辖区内防火单位故障部件总数量
        /// </summary>
        /// <param name="fireDeptId"></param>
        /// <returns></returns>
        Task<List<NumberCard>> GetFaultDetectorNum(int fireDeptId);
        /// <summary>
        /// 获取辖区内防火单位的数量
        /// </summary>
        /// <param name="fireDeptId"></param>
        /// <returns></returns>
        Task<List<NumberCard>> GetFireUnitNum(int fireDeptId);
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
