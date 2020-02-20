using Abp.Application.Services.Dto;
using Abp.Domain.Services;
using FireProtectionV1.FireWorking.Dto;
using FireProtectionV1.FireWorking.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireProtectionV1.FireWorking.Manager
{
    public interface IAlarmManager : IDomainService
    {
        /// <summary>
        /// 新增火警联网数据
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task AddAlarmFire(AddAlarmFireInput input);
        /// <summary>
        /// 查询指定时间以后的最新电气火灾报警数据
        /// </summary>
        /// <param name="startTime"></param>
        /// <returns></returns>
        IQueryable<AlarmToElectric> GetNewElecAlarm(DateTime startTime);
        /// <summary>
        /// 核警处理
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task CheckFirmAlarm(AlarmCheckInput input);
        //IQueryable<AlarmToFire> GetAlarms(IQueryable<Detector> detectors, DateTime start, DateTime end);
        /// <summary>
        /// 获取数据大屏的火警联网实时达
        /// </summary>
        /// <param name="fireUnitId">防火单位Id</param>
        /// <param name="dataNum">需要的数据条数，不传的话默认为5条</param>
        /// <returns></returns>
        Task<List<FireAlarmForDataScreenOutput>> GetFireAlarmForDataScreen(int fireUnitId, int dataNum);
        /// <summary>
        /// 根据fireAlarmId获取单条火警数据详情
        /// </summary>
        /// <param name="fireAlarmId"></param>
        /// <returns></returns>
        Task<FireAlarmDetailOutput> GetFireAlarmById(int fireAlarmId);
        /// <summary>
        /// 获取火警联网数据列表
        /// </summary>
        /// <param name="input"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<PagedResultDto<FireAlarmListOutput>> GetFireAlarmList(FireAlarmListInput input, PagedResultRequestDto dto);
        /// <summary>
        /// 获取防火单位电气火灾警情数据列表
        /// </summary>
        /// <param name="input"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<PagedResultDto<ElectricAlarmListOutput>> GetElectricAlarmList(GetElectricAlarmListInput input, PagedResultRequestDto dto);
        /// <summary>
        /// 获取工程端电气火灾警情数据列表
        /// </summary>
        /// <param name="input"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<PagedResultDto<ElectricAlarmListOutput>> GetElectricAlarmListForEngineer(GetElectricAlarmListForEngineerInput input, PagedResultRequestDto dto);
        /// <summary>
        /// 获取消防管网警情数据列表
        /// </summary>
        /// <param name="input"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<PagedResultDto<WaterAlarmListOutput>> GetWaterAlarmList(GetWaterAlarmListInput input, PagedResultRequestDto dto);
        /// <summary>
        /// 获取防火单位未读警情类型及数量
        /// </summary>
        /// <param name="fireUnitId"></param>
        /// <returns></returns>
        Task<List<GetNoReadAlarmNumOutput>> GetNoReadAlarmNumList(int fireUnitId);
        /// <summary>
        /// 获取工程端未读警情类型及数量
        /// </summary>
        /// <param name="engineerId"></param>
        /// <returns></returns>
        Task<List<GetNoReadAlarmNumOutput>> GetNoReadAlarmNumListForEngineer(int engineerId);
        /// <summary>
        /// 获取区域内各防火单位火警联网在某个时间段内的真实火警报119数据，如果不传year、month，则取全部时间的数据
        /// </summary>
        /// <param name="fireDeptId"></param>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <returns></returns>
        Task<List<GetTrueFireAlarmListOutput>> GetAlarmTo119List(int fireDeptId, int year = 0, int month = 0);
    }
}
