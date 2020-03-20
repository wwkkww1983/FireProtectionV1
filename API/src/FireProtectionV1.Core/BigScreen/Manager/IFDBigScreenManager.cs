using Abp.Domain.Services;
using FireProtectionV1.BigScreen.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FireProtectionV1.BigScreen.Manager
{
    public interface IFDBigScreenManager : IDomainService
    {
        /// <summary>
        /// 监管部门数据大屏：获取防火单位地图点位列表
        /// </summary>
        /// <param name="deptId"></param>
        /// <returns></returns>
        Task<List<GetFireunitLatForMapOutput>> GetFireunitLatForMap(int deptId);
        /// <summary>
        /// 监管部门数据大屏：获取真实火警联网实时达
        /// </summary>
        /// <param name="deptId"></param>
        /// <param name="num"></param>
        /// <returns></returns>
        Task<List<GetTrueFireAlarmList_Output>> GetTrueFireAlarmList(int deptId, int num);
        /// <summary>
        /// 监管部门数据大屏：获取火警联网部件正常率
        /// </summary>
        /// <param name="deptId"></param>
        /// <returns></returns>
        Task<GetFireDetectorStatusNumDto> GetFireDetectorStatusNum(int deptId);
        /// <summary>
        /// 监管部门数据大屏：获取电气火灾防护指标各状态数量
        /// </summary>
        /// <param name="deptId"></param>
        /// <returns></returns>
        Task<GetElectricDeviceStatusNumDto> GetElectricDeviceStatusNum(int deptId);
        /// <summary>
        /// 监管部门数据大屏：获取数据大屏所需显示的其它数量
        /// </summary>
        /// <param name="deptId"></param>
        /// <returns></returns>
        Task<GetOtherNumOutput> GetOtherNum(int deptId);
    }
}
