using Abp.Application.Services.Dto;
using Abp.Domain.Services;
using FireProtectionV1.Enterprise.Dto;
using FireProtectionV1.User.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FireProtectionV1.User.Manager
{
    public interface IFireUnitUserManager : IDomainService
    {
        /// <summary>
        /// 添加防火单位账号
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<int> Add(FireUnitUserInput input);

        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<FireUnitUserLoginOutput> UserLogin(LoginInput input);

        /// <summary>
        /// 获取防火单位工作人员
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<List<GetUnitPeopleOutput>> GetFireUnitPeople(GetUnitPeopleInput input);

        /// <summary>
        /// 获取工作人员详情
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<GetUnitPeopleOutput> GetUserInfo(GetUnitPeopleInput input);
        Task<SuccessOutput> UserRegist(UserRegistInput input);

        /// <summary>
        /// 编辑工作人员信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<SuccessOutput> UpdateUserInfo(GetUnitPeopleOutput input);

        /// <summary>
        /// 新增工作人员
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<SuccessOutput> AddUser(AddUserInput input);

        /// <summary>
        /// 删除工作人员
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<SuccessOutput> DeleteUser(DeleteUserInput input);

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<SuccessOutput> ChangePassword(ChangeUserPassword input);
    }
}
