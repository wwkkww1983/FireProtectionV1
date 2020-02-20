using Abp.Application.Services.Dto;
using Abp.Domain.Services;
using FireProtectionV1.FireWorking.Dto;
using FireProtectionV1.Enterprise.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using FireProtectionV1.Enterprise.Model;

namespace FireProtectionV1.Enterprise.Manager
{
    public interface IFireUnitManager : IDomainService
    {
        Task<List<GetFireUnitTypeOutput>> GetFireUnitTypes();
        /// <summary>
        /// 添加防火单位（同时会添加防火单位管理员账号）
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<SuccessOutput> Add(AddFireUnitInput input);
        /// <summary>
        /// 修改防火单位信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<SuccessOutput> Update(UpdateFireUnitInput input);
        /// <summary>
        /// 删除防火单位
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<SuccessOutput> Delete(FireUnitIdInput input);
        /// <summary>
        /// 防火单位信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<GetFireUnitInfoOutput> GetFireUnitInfo(GetFireUnitInfoInput input);
        Task<List<GetFireUnitExcelOutput>> GetFireUnitListExcel(GetPagedFireUnitListInput input);
        Task<PagedResultDto<GetFireUnitListOutput>> GetFireUnitList(GetPagedFireUnitListInput input);
        Task<PagedResultDto<GetFireUnitListForMobileOutput>> GetFireUnitListForMobile(GetPagedFireUnitListInput input);
        Task<SuccessOutput> InvitatVerify(InvitatVerifyInput input);
        /// <summary>
        /// 根据名称模糊查询防火单位
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<List<FireUnitNameOutput>> QueryFireUnitLikeName(QueryFireUnitLikeNameInput input);

        /// <summary>
        /// 消防部门用户关注防火单位
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<SuccessOutput> AttentionFireUnit(DeptUserAttentionFireUnitInput input);
        Task<string> GetFirePlan(int fireUnitId);

        /// <summary>
        /// 消防部门用户取消关注防火单位
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<SuccessOutput> AttentionFireUnitCancel(DeptUserAttentionFireUnitInput input);
        Task<SuccessOutput> SaveFirePlan(FirePlanInput input);

        /// <summary>
        /// 地图加载所需使用到的防火单位列表数据
        /// </summary>
        /// <returns></returns>
        Task<List<GetFireUnitMapListOutput>> GetMapList();

        /// <summary>
        /// 防火单位引导设置
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<SuccessOutput> UpdateGuideSet(UpdateFireUnitSetInput input);

        /// <summary>
        /// 获取消防系统
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<List<FireSystem>> GetFireSystem();

        /// <summary>
        /// 获取防火单位消防系统
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<List<GetFireUnitSystemOutput>> GetFireUnitSystem(GetFireUnitSystemInput input);

        /// <summary>
        /// 更新防火单位消防系统
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<SuccessOutput> UpdateFireUnitSystem(UpdateFireUnitSystemInput input);

        /// <summary>
        /// 增加消防系统
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<SuccessOutput> AddFireSystem(AddFireSystemInput input);

        /// <summary>
        /// 获取绑定设施编码列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<GetEquipmentNoListOutput> GetEquipmentNoList(GetEquipmentNoListInput input);

        /// <summary>
        /// 修改设施编码信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<SuccessOutput> UpdateEquipmentNoInfo(UpdateEquipmentNoInfoInput input);

        /// <summary>
        /// 绑定设施编码
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<SuccessOutput> AddEquipmentNo(AddEquipmentNoInput input);

        /// <summary>
        /// 扫码获取信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<GetEquipmentNoInfoOutput> GetEquipmentNoInfo(GetEquipmentNoInfoInput input);
        Task<SuccessOutput> DelEquipmentNo(DelEquipmentNoInput input);
    }
}
