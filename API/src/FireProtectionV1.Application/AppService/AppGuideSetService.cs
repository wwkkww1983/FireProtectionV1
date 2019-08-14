using FireProtectionV1.Enterprise.Dto;
using FireProtectionV1.Enterprise.Manager;
using FireProtectionV1.Enterprise.Model;
using FireProtectionV1.User.Dto;
using FireProtectionV1.User.Manager;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FireProtectionV1.AppService
{
    public class AppGuideSet : AppServiceBase
    {
        IFireUnitUserManager _fireUnitAccountManager;
        IFireUnitManager _fireUnitManager;

        public AppGuideSet(IFireUnitUserManager fireUnitAccountManager, IFireUnitManager fireUnitInfoManager)
        {
            _fireUnitAccountManager = fireUnitAccountManager;
            _fireUnitManager = fireUnitInfoManager;
        }

        /// <summary>
        /// 获取防火单位工作人员
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<List<GetUnitPeopleOutput>> GetFireUnitPeople(GetUnitPeopleInput input)
        {
            return await _fireUnitAccountManager.GetFireUnitPeople(input);
        }

        /// <summary>
        /// 获取工作人员详情
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<GetUnitPeopleOutput> GetUserInfo(GetUnitPeopleInput input)
        {
            return await _fireUnitAccountManager.GetUserInfo(input);
        }
        /// <summary>
        /// 编辑工作人员信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<SuccessOutput> UpdateUserInfo(GetUnitPeopleOutput input)
        {
            return await _fireUnitAccountManager.UpdateUserInfo(input);
        }

        /// <summary>
        /// 新增工作人员
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<SuccessOutput> AddUser(AddUserInput input)
        {
            return await _fireUnitAccountManager.AddUser(input);
        }

        /// <summary>
        /// 删除工作人员
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<SuccessOutput> DeleteUser(DeleteUserInput input)
        {
            return await _fireUnitAccountManager.DeleteUser(input);
        }

        /// <summary>
        /// 更新防火单位引导设置
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns> 
        public async Task<SuccessOutput> UpdateGuideSet(UpdateFireUnitSetInput input)
        {
            return await _fireUnitManager.UpdateGuideSet(input);
        }

        /// <summary>
        /// 获取消防系统
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<List<FireSystem>> GetFireSystem()
        {
            return await _fireUnitManager.GetFireSystem();
        }

        /// <summary>
        /// 获取防火单位消防系统
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<List<GetFireUnitSystemOutput>> GetFireUnitSystem(GetFireUnitSystemInput input)
        {
            return await _fireUnitManager.GetFireUnitSystem(input);
        }

        /// <summary>
        /// 更新防火单位消防系统
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<SuccessOutput> UpdateFireUnitSystem(UpdateFireUnitSystemInput input)
        {
            return await _fireUnitManager.UpdateFireUnitSystem(input);
        }

        /// <summary>
        /// 增加消防系统
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<SuccessOutput> AddFireSystem(AddFireSystemInput input)
        {
            return await _fireUnitManager.AddFireSystem(input);
        }
        /// <summary>
        /// 绑定设施编码
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<SuccessOutput> AddEquipmentNo(AddEquipmentNoInput input)
        {
            return await _fireUnitManager.AddEquipmentNo(input);
        }
        /// <summary>
        /// 扫码获取信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<GetEquipmentNoInfoOutput> GetEquipmentNoInfo(GetEquipmentNoInfoInput input)
        {
            return await _fireUnitManager.GetEquipmentNoInfo(input);
        }
    }
}
