using FireProtectionV1.BigScreen.Dto;
using FireProtectionV1.BigScreen.Manager;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FireProtectionV1.AppService
{
    public class FDBigScreenAppService : AppServiceBase
    {
        IFDBigScreenManager _fdManager;
        public FDBigScreenAppService(IFDBigScreenManager fdManager)
        {
            _fdManager = fdManager;
        }
        /// <summary>
        /// 监管部门数据大屏：获取防火单位地图点位列表
        /// </summary>
        /// <param name="deptId"></param>
        /// <returns></returns>
        public async Task<List<GetFireunitLatForMapOutput>> GetFireunitLatForMap(int deptId)
        {
            return await _fdManager.GetFireunitLatForMap(deptId);
        }
        /// <summary>
        /// 监管部门数据大屏：获取真实火警联网实时达
        /// </summary>
        /// <param name="deptId"></param>
        /// <param name="num">希望获取的条数</param>
        /// <returns></returns>
        public async Task<List<GetTrueFireAlarmList_Output>> GetTrueFireAlarmList(int deptId, int num)
        {
            return await _fdManager.GetTrueFireAlarmList(deptId, num);
        }
        /// <summary>
        /// 监管部门数据大屏：获取火警联网部件正常率
        /// </summary>
        /// <param name="deptId"></param>
        /// <returns></returns>
        public async Task<GetFireDetectorStatusNumDto> GetFireDetectorStatusNum(int deptId)
        {
            return await _fdManager.GetFireDetectorStatusNum(deptId);
        }
        /// <summary>
        /// 监管部门数据大屏：获取电气火灾防护指标各状态数量
        /// </summary>
        /// <param name="deptId"></param>
        /// <returns></returns>
        public async Task<GetElectricDeviceStatusNumDto> GetElectricDeviceStatusNum(int deptId)
        {
            return await _fdManager.GetElectricDeviceStatusNum(deptId);
        }
        /// <summary>
        /// 监管部门数据大屏：获取数据大屏所需显示的其它数量
        /// </summary>
        /// <param name="deptId"></param>
        /// <returns></returns>
        public async Task<GetOtherNumOutput> GetOtherNum(int deptId)
        {
            return await _fdManager.GetOtherNum(deptId);
        }
    }
}
